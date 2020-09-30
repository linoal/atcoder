using System;
using System.Collections.Generic;
using System.Linq;

namespace AtcoderLib{
// 参考サイト： https://www.hanachiru-blog.com/entry/2020/06/19/141057


    public class SegmentTree<T>{

        public int N { get; private set; }
        private T[] tree;
        private readonly Func<T,T,T> updateMethod;
        private readonly T initValue;

        // セグメント木の初期化
        // arg: 要素の配列, 更新する方法(x,y)=>val(minとかsumとか), ノードの初期値
        public SegmentTree(IEnumerable<T> items, Func<T,T,T> _updateMethod, T _initValue){
            T[] array = items.ToArray();
            updateMethod = _updateMethod;
            initValue = _initValue;

            // セグ木の最下段は2のべき乗にする
            N = 1;
            while (N < array.Length) N *= 2;
            tree = Enumerable.Repeat(initValue, 2 * N - 1).ToArray();

            // 最下段に要素配列を入れたあと、下の段から更新する
            for (int i = 0; i < array.Length; i++) tree[N + i - 1] = array[i];
            for (int i = N - 2; i >= 0; i--) tree[i] = updateMethod(tree[2 * i + 1], tree[2 * i + 2]);
        }

        // 更新する
        // arg: 更新したい値のインデックス、更新する値
        public void Update(int index, T val){
            int i = N + index - 1; // 更新したい要素のセグ木上のインデックスを取得
            // 値を更新したあとに、次々に親を更新していく
            tree[i] = val;
            while(i > 0){
                i = (i - 1) / 2;
                tree[i] = updateMethod(tree[2 * i + 1], tree[2 * i + 2]);
            }
        }

        // 区間内の目的の値を取得する。区間 [a,b) bは開区間であることに注意。
        public T Execute(int a, int b) => Execute(a, b, 0, 0, N);

        private T Execute(int a, int b, int k, int l, int r){
            // 要求区間 [a,b) に対して対象区間 [l,r)を求める。
            // 今いるノードのインデックスがk

            // 要求区間と対象区間が交わらない
            if(r <= a || b <= l) return initValue;
            // 要求区間が対象区間を完全に被覆
            if(a <= l && r <= b) return tree[k];

            // 要求区間が対象区間の一部を被覆しているので、子について探索する
            // 新しい対象区間は、現在の対象区間を半分に割ったもの
            var vl = Execute(a, b, 2 * k + 1, l, (l + r) / 2);
            var vr = Execute(a, b, 2 * k + 2, (l + r) / 2, r);
            return updateMethod(vl, vr);
        }
    }
}