﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ACLB{
    class SolverC{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverC().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{

                (int N, int M) = Get.Tuple<int,int>();
                UnionFind uf = new UnionFind(N);
                for(int i=0; i<M; i++){
                    (int a, int b) = Get.Tuple<int,int>();
                    a--; b--;
                    uf.Union(a,b);
                }
                int groupNum = 0;
                for(int i=0; i<N; i++){
                    if(uf.Parents[i] < 0){
                        groupNum++;
                    }
                }
                WriteLine(groupNum-1);
            }
        }


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

        
        private static class Get
        {
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
