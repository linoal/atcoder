using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;
using System.Numerics;

namespace abc177{
    class SolverE{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                // BigInteger MOD = 46116646144580591;
                // int N = Get.Int();
                // ulong[] A = Get.Longs().Select(x => (ulong)x).ToArray();
                // // BigInteger mul = 1;
                // BigInteger lcm = 1;
                // BigInteger gcd = A[0];
                // bool isPairwise = true;
                // for(int i=0; i<N; i++){
                //     // mul *= A[i];
                //     BigInteger tmpLcm = lcm;
                //     lcm = Lcm(lcm, A[i]) % MOD;
                //     if( tmpLcm * A[i] % MOD != lcm && gcd != 1){
                //         isPairwise = false;
                //     }
                //     gcd = Gcd(gcd, A[i]);
                // }
                // if( isPairwise ){
                //     WriteLine("pairwise coprime");
                // }else if (gcd == 1){
                //     WriteLine("setwise coprime");
                // }else{
                //     WriteLine("not coprime");
                // }
                var N = Get.Int();
                var A = Get.Ints();

                var fpf = new FasterPrimeFactorization(A.Max());
                Dictionary<int,int> dic = new Dictionary<int, int>();
                for(int i=0; i<N; i++){
                    var factList = fpf.Factorization(A[i]);
                    for(int j=0; j<factList.Count; j++){
                        var fact = factList[j];
                        var prime = fact.prime;
                        // var exp = fact.exp;
                        if (dic.ContainsKey(prime)){
                            dic[prime] += 1;
                        }else{
                            dic.Add(prime, 1);
                        }
                    }
                }

                bool isPairwise = true;
                bool isCoprime = true;

                foreach(var d in dic){
                    // Debug.Put(d.Key,"prime",d.Value,"num");
                    if(d.Value > 1) isPairwise = false;
                    if(d.Value == N) isCoprime = false;
                }
                if(isPairwise){
                    WriteLine("pairwise coprime");
                }else if(!isCoprime){
                    WriteLine("not coprime");
                }else{
                    WriteLine("setwise coprime");
                }

            }
        }

        // 高速素因数分解。
        // 下処理が O(AloglogA)、素因数分解が O(logA) (A = 素因数分解したい数のうち最大の数)
        class FasterPrimeFactorization{
            int[] spf; // smallest prime factorsのテーブル

            // 下処理でspfテーブルを作る。引数は素因数分解したい A_1 ～ A_N の最大値 aMax。
            public FasterPrimeFactorization(int aMax){
                spf = new int[aMax+1];
                for(int i=0; i<spf.Length; i++){
                    spf[i] = i;
                }

                for(int i=2; i*i <= aMax; i++){
                    if (spf[i] == i){
                        for(int j = i*i; j <= aMax; j += i){
                            if (spf[j] == j){
                                spf[j] = i;
                            }
                        }
                    }
                }
                // Debug.Put(spf,"spf");
            }

            // 引数 a を素因数分解する。戻り値は List<(素数, 指数)> の形式。
            public List<(int prime, int exp)> Factorization(int a){
                var primes = new List<(int prime, int exp)>();
                while (a != 1){
                    int prime = spf[a];
                    int exp = 0;
                    while (spf[a] == prime){
                        exp++;
                        a /= prime;
                    }
                    primes.Add((prime: prime, exp: exp));
                    // Debug.Put(prime,"prime", exp,"exp");
                }
                return primes;
            }
        }

        BigInteger Lcm(BigInteger a, BigInteger b){
            return a / Gcd(a,b) * b;
        }

        BigInteger Gcd(BigInteger m, BigInteger n){
            if(m<n){
                BigInteger tmp = n;
                n = m;
                m = tmp;
            }
            if(n==0) return m;
            return Gcd(n, m%n); 
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
