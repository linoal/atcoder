using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;

namespace AtcoderLib{
    class UnionFind{
        // 自分が親であるとき、その集合の要素数に-1を掛けた値を保持する。
        // そうでなければ親のidを保持する。
        public int[] Parents {get; set;}

        public UnionFind(int n){
            Parents = new int[n];
            for(int i=0; i<n; i++){
                Parents[i] = -1;
            }
        }

        // 要素xのルートを返す
        public int Find(int x){
            if (Parents[x] < 0) return x;
            // ルートまで再帰的に探し、ルートにつなぎ替える
            Parents[x] = Find(Parents[x]);
            return Parents[x];
        }

        // 要素xの属するグループの要素数
        public int Size(int x){
            return -Parents[Find(x)];
        }

        public bool Same(int x, int y){
            return Find(x) == Find(y);
        }

        public bool Union(int x, int y){
            x = Find(x);
            y = Find(y);
            if (x == y) return false;

            // 要素数が少ないグループを多いほうに書き換えたい
            if (Size(x) < Size(y)){
                var tmp = x;
                x = y;
                y = tmp;
            }

            // まとめる先の要素数を更新
            Parents[x] += Parents[y];
            // まとめられるグループのルートの親を書き換え
            Parents[y] = x;
            return true;
        }
        
    }
}