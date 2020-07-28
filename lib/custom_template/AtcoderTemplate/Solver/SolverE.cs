using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace PROJECT_NAME
{
    class SolverE
    {
        static void Main()
        {
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve()
        {
            var factors = Prime.PrimeFactorization(Get.Long());
            long ans = 0;
            foreach (var fact in factors)
            {
                // WriteLine($"{fact.prime}, {fact.exp}");
                long exp = fact.exp;
                int i = 1; int s = 0;
                while(true)
                {
                    if( s + i <= exp )
                    {
                        s += i++;
                        ans++;
                    }else break;
                }
            }
            WriteLine(ans);
            
        }

        static class Prime
            {
                // 約数は昇順になる
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
