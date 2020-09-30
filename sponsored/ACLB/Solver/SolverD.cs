using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ACLB{
    class SolverD{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverD().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                int JMAX = 300000;

                (int N, int K) = Get.Tuple<int,int>();
                var A = new int[N];
                for(int i=0; i<N; i++){
                    A[i] = Get.Int();
                }
                var initjs = Repeat<long>(0,JMAX+1).ToArray();
                var seg = new SegmentTree<long>(initjs, (x,y)=>Max(x,y), 0);
                for(int i=0; i<N; i++){
                    var rnmax = seg.Execute((int)Max(0,A[i]-K),(int)Max(K,A[i]+K+1));
                    seg.Update(A[i],rnmax+1);
                }
                long max = 0;
                for(int j=0; j<JMAX; j++){
                    max = Max(max, seg.Execute(j,j+1));
                }
                WriteLine(max);


            }
        }

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
    } // class SegmentTree ここまで



        static class Mod{
            public static long Pow(long x, long e, long mod = long.MaxValue){
                long res = 1;
                while (e > 0){
                    if ((e & 1) == 1) res = res * x % mod;
                    x = x * x % mod;
                    e >>= 1;
                }
                return res;
            }

            // 逆元を求める。前提：modが素数、aがpの倍数でない。フェルマーの小定理に基づく。
            public static long Inv(long a, long mod){
                return Pow(a, mod-2, mod);
            }
        }
        
        static class Debug{
            public static void Put(object obj, int padLeft = 0, bool newline = true){

                if (obj is Array arr){
                    if( arr.Rank == 1 ){
                        Write("[");
                        for(int i=0; i<arr.Length; i++){
                            Put(arr.GetValue(i), padLeft, false);
                            if( i < arr.Length - 1 ) Write(", ");
                        }
                        Write("]");
                    }
                    else if( arr.Rank == 2 ){
                        Write("[\n");
                        for(int i=0; i<arr.GetLength(0); i++){
                            Write(" [");
                            for(int j=0; j<arr.GetLength(1); j++){
                                Put(arr.GetValue(i,j), padLeft, false);
                                if (j < arr.GetLength(1) - 1) Write(", ");
                            }
                            if (i < arr.GetLength(0) - 1 ) Write("],\n");
                            else Write("]\n");
                        }
                        Write("]");
                    }
                }
                else if( obj is ValueType val ){
                    Write(val.ToString().PadLeft(padLeft));
                }
                else if( obj is string str ){
                    Write(str.PadLeft(padLeft));
                }
                else{
                    Write(obj.ToString().PadLeft(padLeft));
                }

                if(newline) Write("\n");
            }

            public static void Put(object obj, string label, int padLeft = 0, bool newline = true){
                Write("\u001b[32m{0}:\u001b[0m ", label);
                Put(obj, padLeft, newline);
            }
            public static void Put(params object[] args){
                if (args.Length % 2 != 0){
                    WriteLine("Debug.Put(params): arg length shall be multiple of 2.");
                }
                for(int i=0; i<args.Length; i += 2){
                    Put(args[i], (string)args[i+1],0, false);
                    Write("   ");
                }
                Write("\n");
            }
        }

        
        private static class Get{
            public static string Str() => ReadLine().Trim();
            public static int Int() => int.Parse(Str());
            public static long Long() => long.Parse(Str());
            public static double Double() => double.Parse(Str());
            public static string[] Strs() => Str().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            public static int[] Ints() => Strs().Select(int.Parse).ToArray();
            public static long[] Longs() => Strs().Select(long.Parse).ToArray();
            public static double[] Doubles() => Strs().Select(double.Parse).ToArray();
            static bool TypeEq<T,U>() => typeof(T).Equals(typeof(U));
            static T TypeConv<T,U>(U u) => (T)Convert.ChangeType(u, typeof(T));
            static T TypeConv<T>(string s) => TypeEq<T, int>() ?   TypeConv<T, int>(int.Parse(s))
                                        : TypeEq<T, long>() ?       TypeConv<T, long>(long.Parse(s))
                                        : TypeEq<T, double>() ?     TypeConv<T, double>(long.Parse(s))
                                        : TypeConv<T, string>(s);
            public static (T,U) Tuple<T,U>() {string[] strs = Strs(); T t = TypeConv<T>(strs[0]); U u = TypeConv<U>(strs[1]); return(t,u);}
            public static (T,U,V) Tuple<T,U,V>() {string[] strs = Strs(); T t = TypeConv<T>(strs[0]); U u = TypeConv<U>(strs[1]); V v = TypeConv<V>(strs[2]); return(t,u,v);}
        }
    }
}
