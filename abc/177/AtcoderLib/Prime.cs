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