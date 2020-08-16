using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC175
{
    class SolverD
    {
        static void Main()
        {
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverD().Solve();
            Out.Flush();
        }

        public void Solve()
        {          
            (long N, long K) = Get.Tuple<long,long>();
            long[] P = Get.Longs().Select(x => x-1).ToArray();
            long[] C = Get.Longs();

            var graph = new Graph(N,P,C);
            WriteLine(graph.CalcScore(N,K,C,P));
        }

        class PointLoop{
            public long loopN;
            public long loopP;
            public List<long> pointByCycle = new List<long>();

        }

        class Graph{
            public PointLoop[] pls;

            public Graph(long N, long[] P, long[] C){
                pls = new PointLoop[N];
                for(int p=0; p<N; p++){
                    var pl = new PointLoop();
                    pls[p] = pl;
                    pl.loopP = 0;

                    long currentPos = p;
                    for(long v=0; v<N; v++){
                        currentPos = P[currentPos];
                        pl.loopP += C[currentPos];
                        pl.pointByCycle.Add(C[currentPos]);
                        if(currentPos == p){
                            pl.loopN = v+1;
                            break;
                        }
                    }
                }
            }

            public long CalcScore(long N, long K, long[] C, long[] P){
                long maxSp = long.MinValue;
                for(long p=0; p<N; p++){
                    var pl = pls[p];

                    long sc = 0;
                    for(int c=0; c<pl.loopN; c++){
                        if(c+1>K) break;
                        long sl = 0;
                        if(pl.loopP > 0){
                            sl = (K - (c+1)) / pl.loopN * pl.loopP;
                        }
                        sc += pl.pointByCycle[(int)((c+1)%pl.loopN)];
                        maxSp = Max(maxSp, sc+sl);
                    }
                }
                return maxSp;
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
