using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;


namespace msolutions2020
{
    class SolverE
    {
        static Dictionary<int,int>[] distDictX;
        static Dictionary<int,int>[] distDictY;
        static int N;
        static Village[] V;
        static void Main()
        {
            Solve();
        }

        public static void Solve()
        {
            N = Get.Int();
            V = new Village[N];
            foreach(int i in Range(0,N))
            {
                (int x,int y,int p)vil = Get.Tuple<int,int,int>();
                V[i].x = vil.x;
                V[i].y = vil.y;
                V[i].p = vil.p;
            }

            distDictX = new Dictionary<int, int>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                distDictX[i] = new Dictionary<int,int>(60_000);
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnX = 0; ptnX < IntPow(2,(uint)N); ptnX++)
                {
                    Dim1Pattern ptn = new Dim1Pattern(ptnX, N);
                    distDictX[vil].Add(ptnX, ptn.DistX(V[vil], V));
                }
            }

            distDictY = new Dictionary<int, int>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                distDictY[i] = new Dictionary<int,int>(60_000);
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnY = 0; ptnY < IntPow(2,(uint)N); ptnY++)
                {
                    Dim1Pattern ptn = new Dim1Pattern(ptnY, N);
                    distDictY[vil].Add(ptnY, ptn.DistY(V[vil], V));
                }
            }

            // WriteLine("pre dict generated.");
            
            for (int k = 0; k <= N; k++)
            {
                long minCost = long.MaxValue;
                var ptn = new Pattern(N);
                while(ptn.next(k) != null)
                {
                    minCost = Min(minCost, cost(ptn, V, k));
                }
                WriteLine(minCost);
            }          
        }

        static int IntPow(int x, uint pow)
        {
            int ret = 1;
            while ( pow != 0 )
            {
                if ( (pow & 1) == 1 )
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        static long cost(Pattern ptn, Village[] V, int k)
        {
            if(ptn.trainNum != k ) return long.MaxValue;
            int vLength = V.Length;
            long cost = 0;
            for (int v = 0; v < vLength; v++)
            {
                int ptx = 0;
                for(int i=0 ; i<vLength; i++)
                {
                    ptx = ptn[i] == 1 ?(ptx | (1<<i)) : ptx;
                }
                // Dim1Pattern ptnx = new Dim1Pattern(ptx, V.Length);
                
                int pty = 0;
                for(int i=0 ; i<vLength; i++)
                {
                    pty = ptn[i] == 2 ? (pty | (1<<i)) : pty;
                }
                // Dim1Pattern ptny = new Dim1Pattern(pty, V.Length);

                cost +=Min(distDictX[v][ptx], distDictY[v][pty]) * V[v].p;
            }
            // WriteLine(string.Join(", ", ptn.pattern) + $"  train:{ptn.trainNum}   cost:{cost}");
            return cost;
        }

        struct Village
        {
            public int x;
            public int y;
            public int p;
        }


        class Pattern
        {
            
            public int[] pattern;
            public int trainNum = 0;
            public Pattern(int N)
            {
                pattern = Repeat(0,N+1).ToArray();
                pattern[0] = -1;
            }

            public int[] next(int k)
            {
                pattern[0]++;
                
                if(pattern[0] == 1)
                {
                    trainNum++;
                     if(trainNum > k) pattern[0] = 3; // k本を超えたらその1の桁は探索スキップするため繰り上げる
                }
                for (int i = 0; i < pattern.Length ; i++)
                {
                    if(pattern[i] == 1 && pattern[i] > k) pattern[i]=3; // 桁スキップ
                    // if(pattern[i] <= 2) // 繰り上がらない
                    // {
                    //     // if( trainNum > k) // k本を超えたらその桁は探索スキップ
                    //     // {
                    //     //     pattern[i] = 3;
                    //     // }
                    // }
                    if(pattern[i] > 2) // 繰り上がる
                    {
                        pattern[i] = 0; trainNum--;
                        pattern[i+1]++;
                        if(pattern[i+1] == 1) trainNum++;
                        if(pattern.Last() == 1) return null;
                    }
                }
                // WriteLine("pattern: " + string.Join(", ", pattern) + "   train: " + trainNum);
                return pattern;
            }

            public int this[int i]
            {
                get{ return pattern[i]; }
            }

        }

        class Dim1Pattern
        {
            public int pattern;
            int N;

            public Dim1Pattern(int _pattern, int n)
            {
                pattern = _pattern;
                N = n;
            }

            

            public int DistX(Village v, Village [] Vils)
            {
                int dist = Abs(v.x);
                for(int i=0; i<Vils.Length; i++)
                {
                    if(((pattern >> i) & 1) == 1)
                    {
                        dist = Min(dist, Abs(Vils[i].x - v.x));
                    }
                }
                return dist;
            }

            public int DistY(Village v, Village [] Vils)
            {
                int dist = Abs(v.y);
                for(int i=0; i<Vils.Length; i++)
                {
                    if(((pattern >> i) & 1) == 1)
                    {
                        dist = Min(dist, Abs(Vils[i].y - v.y));
                    }
                }
                return dist;
            }

        }


        static class Get
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
