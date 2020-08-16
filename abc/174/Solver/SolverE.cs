using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC174
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
            (long N, long K) = Get.Tuple<long,long>();
            long[] A = Get.Longs();
            long ml = 1;
            long mr = A.Max();
            while(mr != ml){
                long ac = (mr + ml) / 2;
                if( cutNumInLen(ac, A) > K){
                    ml = ac + 1;
                }else{
                    mr = ac;
                }
                // WriteLine($"mr={mr}, ml={ml}");
            }
            WriteLine(mr);
            
        }

        long cutNumInLen(long m, long[] A){
            long cut = 0;
            for (int i = 0; i < A.Length; i++)
            {
                long k;
                if( A[i] % m == 0){
                    k = A[i] / m - 1;
                }else{
                    k = A[i] / m;
                }
                cut += k;
            }
            // WriteLine($"cutnuminlen m={m} cut={cut}");
            return cut;
        }

        class Log : IComparable{
            public long num;
            public long defaultLength;
            public long length;
            public long cut = 0;

            public Log(long _num, long _length, long _defaultLength){
                this.num = _num;
                this.length = _length;
                defaultLength = _defaultLength;
            }

            public int CompareTo(Object otherObj){
                if( otherObj is Log other){
                    return length.CompareTo(other.length);
                }
                return -1;
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
