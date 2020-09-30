using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;

namespace AtcoderLib
{
    static class Prime
    {

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
                }
                return primes;
            }
        }

        static long Lcm(long a, long b){
            return a / Gcd(a,b) * b;
        }

        static long Gcd(long m, long n){
            if(m<n){
                long tmp = n;
                n = m;
                m = tmp;
            }
            if(n==0) return m;
            return Gcd(n, m%n); 
        }



        /// 素因数分解。List<(素数, 指数)> の形式で返す。
        /// List内は素数に関して昇順。
        public static List<(long prime, long exp)> PrimeFactorization(long N)
        {
            var primes = new List<(long prime, long exp)>();
            long n = N;
            for (long i=2; i * i < N; i++)
            {
                long exp = 0;
                while (n % i == 0)
                {
                    n /= i;
                    exp++;
                }
                if (exp > 0)
                {
                    primes.Add((prime: i, exp: exp));
                }
            }
            if (n != 1) primes.Add((prime: n, exp: 1));
            return primes;
        }
    }
}