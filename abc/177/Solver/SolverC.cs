﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace abc177{
    class SolverC{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverC().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                long MOD = Mod.Pow(10,9)+7;

                int N = Get.Int();
                long[] A = Get.Longs();
                // long[] S = new long[N];
                // long sum=0;
                // for(int i=N-1; i>=0; i--){
                //     S[i] = sum;
                //     sum += A[i];
                //     sum %= MOD;
                    
                // }
                // long ans=0;
                // for(int i=0; i<N; i++){
                //     ans += A[i]*S[i];
                //     ans %= MOD;
                // }
                // WriteLine(ans);

                long sum = 0;
                for(int i=0; i<N; i++){
                    sum += A[i];
                    sum %= MOD;
                }
                // Debug.Put(sum,"sum");
                long area = sum * sum % MOD;
                for(int i=0; i<N; i++){
                    area -= A[i]*A[i] % MOD;
                    if(area<0) area+=MOD;
                }
                // Debug.Put(area,"area");
                // Debug.Put(Mod.Inv(2,MOD),"2の逆元:");
                area = area * Mod.Inv(2,MOD) % MOD;
                WriteLine(area);
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
