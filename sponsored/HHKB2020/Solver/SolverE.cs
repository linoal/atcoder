﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using HHKB2020.SolverEExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace HHKB2020{
    class SolverE{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                long m = 1_000_000_007;
                
                (int H, int W) = Get.Tuple<int,int>();
                string[] S = Get.Lines<string>(H);

                long K = 0;
                for(int i=0; i<H; i++){
                    for(int j=0; j<W; j++){
                        if(S[i][j]=='.') K++;
                    }
                }

                int[,] lh = new int[H,W].Fill(-1);
                // Debug.Put(lv,"lv");
                for(int i=0; i<H; i++){
                    int seq = 0;
                    for(int j=0; j<W; j++){
                        if(S[i][j] == '#'){
                            seq = 0;
                        }else{
                            lh[i,j] = seq;
                            seq++;
                        }
                    }
                }
                // Debug.Put(lh,"lh");
                for(int i=H-1; i>=0; i--){
                    int num = -1;
                    for(int j=W-1; j>=0; j--){
                        if(lh[i,j]==-1){
                            num = -1;
                        }else{
                            num = Max(num, lh[i,j]);
                            lh[i,j] = num;
                        }
                    }
                }
                // Debug.Put(lh,"lh");

                
                int[,] lv = new int[H,W].Fill(-1);
                for(int i=0; i<W; i++){
                    int seq = 0;
                    for(int j=0; j<H; j++){
                        if(S[j][i] == '#'){
                            seq = 0;
                        }else{
                            lv[j,i] = seq;
                            seq++;
                        }
                    }
                }
                // Debug.Put(lv,"lv");
                for(int i=W-1; i>=0; i--){
                    int num = -1;
                    for(int j=H-1; j>=0; j--){
                        if(lv[j,i]==-1){
                            num = -1;
                        }else{
                            num = Max(num, lv[j,i]);
                            lv[j,i] = num;
                        }
                    }
                }
                // Debug.Put(lv,"lv");
                long ans = 0;
                long pow2K = Mod.Pow(2, K, m);

                for(int h=0; h<H; h++){
                    for(int w=0; w<W; w++){
                        if(S[h][w]=='#') continue;
                        long mas = pow2K;
                        long lm = lh[h,w] + lv[h,w] + 1;
                        mas -= Mod.Pow(2, K - lm, m);
                        ans += mas % m;
                        ans %= m;
                        if(ans<0) ans+=m;
                    }
                }

                WriteLine(ans%m);
            }
        }


        // === ここからライブラリ
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
            public static (T,U,V,W) Tuple<T,U,V,W>() {string[] strs = Strs(); T t = TypeConv<T>(strs[0]); U u = TypeConv<U>(strs[1]); V v = TypeConv<V>(strs[2]); W w = TypeConv<W>(strs[3]); return(t,u,v,w);}
            public static T[] Lines<T>(int N){
                T[] ret = new T[N];
                for(int i=0; i<N; i++){ ret[i] = TypeConv<T>(Str()); }
                return ret;
            }
        }
    }

    // 同じ拡張メソッドは同一namespace内で定義できないのでnamespaceを問題ごとに分ける
    namespace SolverEExtensions{
        static class ArrayExtensions{
            public static T[] Fill<T>(this T[] arr, T val){
                for(int i=0; i<arr.Length; i++){
                    arr[i] = val;
                }
                return arr;
            }
            public static T[,] Fill<T>(this T[,] arr, T val ){
                for(int i=0; i<arr.GetLength(0); i++){
                    for(int j=0; j<arr.GetLength(1); j++){
                        arr[i,j] = val;
                    }
                }
                return arr;
            }
        }
    }
    
}
