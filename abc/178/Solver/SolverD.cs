using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC178{
    class SolverD{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverD().Solve();
            Out.Flush();
        }

        long[] dp;
        long m = Mod.Pow(10,9)+7;

        public void Solve(){
            checked{


                long S = Get.Int();
                long ans = 0;
                var comb = new Combination(S,m);
                for(int n=1; 3*n<=S; n++){
                    ans += comb.Get(S-2*n-1, n-1);
                    ans %= m;
                }
                WriteLine(ans);

                // long S = Get.Int();
                // if(S<3){
                //     WriteLine(0);
                //     return;
                // }
                // dp = new long[S+1];
                // dp[0]=1; dp[1]=0; dp[2]=0;
                // for(int i=3; i<=S; i++){
                //     dp[i] = dp[i-1] + dp[i-3];
                //     dp[i] %= m;
                // }
                // WriteLine(dp[S]%m);

                // long S = Get.Int();

                // dp = new long[S+2, S/3 + 2];
                // for(int i=0; i<dp.GetLength(0); i++){
                //     for(int j=0; j<dp.GetLength(1); j++){
                //         if(i<3) dp[i,j] = 0;
                //         else if(j==1) dp[i,j] = 1;
                //         else dp[i,j] = -1;
                //     }
                // }

                
                // long ans = 0;
                // for(int i=1; i<=S/3+1; i++){
                //     var cal = Calc(S,i);
                //     //Debug.Put(cal, $"Calc({S},{i}) = ");
                //     ans += cal;
                //     ans %= m;
                // }
                // WriteLine(ans % m);

            }
        }

        public class Combination{
            long[][] pascal;

            // nCk に関するdpテーブル(要素数 n*n/2)を作成する。O(n^2)。
            public Combination(long n, long mod = long.MaxValue){
                pascal = new long[n+1][];
                for(int i=0; i<n+1; i++){
                    pascal[i] = new long[i+1];
                    pascal[i][0] = 1;
                    pascal[i][i] = 1;
                }
                for(int i=2; i<n+1; i++){
                    for(int j=1; j<i; j++){
                        // ここで左右対称を利用すると、実行時間が半分になるが未実装。
                        pascal[i][j] = ( pascal[i-1][j-1] + pascal[i-1][j] ) % mod;
                    }
                }
            }

            // nCk をdpテーブルから取得する。O(1)。
            public long Get(long n, long k){
                return pascal[n][k];
            }

            // pascal dpテーブルを表示
            public void DebugPrint(){
                for(int i=0; i<pascal.Length; i++){
                    for(int j=0; j<pascal[i].Length; j++){
                        Console.Write($"{pascal[i][j]} ");
                    }
                    Console.Write("\n");
                }
            }
        }

        // public long Calc(long s, long n){
            
        //     if( n*3 > s ) return 0;
        //     if( dp[s,n] != -1 ) {
        //         //Debug.Put("dp hit");
        //         return dp[s,n];
        //         }
        //     if( n == 1 ) return 1;
        //     if( n== 2) return s-5;
        //     if(n==3){
        //         var cal = 0;
        //         for(int i=3; i<=n-3; i++){
        //             cal += i-5;
        //         }
        //         dp[s,n] = cal % m;
        //         return cal % m;
        //     }
        //     long ret = 0;
        //     for(int i=3; i <= s-3; i++){
        //         ret += Calc(s-i, n-1);
        //         ret %= m;
        //     }
        //     dp[s,n] = ret % m;
        //     //Debug.Put(dp); Debug.Put(dp[s,n], $"dp[{s},{n}]");// ReadLine();
        //     return ret % m;
        // }



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
