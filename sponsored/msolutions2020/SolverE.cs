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
        static Dictionary<long,int>[] distDictX;
        static Dictionary<long,int>[] distDictY;
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
            long[] ans = Repeat(long.MaxValue, N+1).ToArray();
            foreach(int i in Range(0,N))
            {
                (int x,int y,int p)vil = Get.Tuple<int,int,int>();
                V[i].x = vil.x;
                V[i].y = vil.y;
                V[i].p = vil.p;
            }

            distDictX = new Dictionary<long, int>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                distDictX[i] = new Dictionary<long,int>(60_0000);
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnX = 0; ptnX < IntPow(2,(uint)N); ptnX++)
                {
                    int ptnKey = 0;
                    for(int i=0; i<N; i++)
                    {
                        ptnKey = (ptnX >> i & 1)==1 ? (ptnKey | (1 << 2*i)) : ptnKey;
                    }
                    Dim1Pattern ptn = new Dim1Pattern(ptnKey, N);
                    // WriteLine($"x: ptnKey={Convert.ToString(ptnKey,2)}");
                    distDictX[vil].Add(ptnKey, ptn.DistX(V[vil], V));
                }
            }

            distDictY = new Dictionary<long, int>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                
                distDictY[i] = new Dictionary<long,int>(60_0000);
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnY = 0; ptnY < IntPow(2,(uint)N); ptnY++)
                {
                    int ptnKey = 0;
                    for(int i=0; i<N; i++)
                    {
                        ptnKey = (ptnY >> i & 1)==1 ? (ptnKey | (1 << 2*i+1)) : ptnKey;
                    }
                    Dim1Pattern ptn = new Dim1Pattern(ptnY, N);
                    // WriteLine($"y: ptnKey={Convert.ToString(ptnKey,2)}");
                    distDictY[vil].Add(ptnKey, ptn.DistY(V[vil], V));
                }
            }

            // WriteLine("pre dict generated.");
            
            // for (int k = 0; k <= N; k++)
            // {
            //     long minCost = cost(0, V, k);
                //IEnumerable<long> patterns = Pattern.Comobination(0,k,0,0,N);

                // foreach(long ptn in Pattern.Comobination(0,k,0,0, N) )
                // {
                //     // WriteLine($"ptn: {Convert.ToString(ptn,2).PadLeft(N*2, '0')}");
                //     minCost = Min(minCost, cost(ptn, V, k));
                // }
                // WriteLine(minCost);
            // }

            Pattern pattern = new Pattern(N);
            while(pattern.Next() != null)
            {
                int t = pattern.trainNum;
                ans[t] = Min(ans[t], cost(pattern.pattern, V));
            }

            foreach(var a in ans)
            {
                WriteLine(a);
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

        static long xmask = 0b_010101010101010101010101010101;
        static long ymask = 0b_101010101010101010101010101010;
        static long cost(long ptn, Village[] V)
        {

            int vLength = V.Length;
            long cost = 0;
            
            for (int v = 0; v < vLength; v++)
            {
                long ptx = ptn & xmask;
                long pty = ptn & ymask;

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
            public long pattern;
            public int trainNum = 0;
            int N;

            public Pattern(int _n)
            {
                pattern = -1;
                N = _n;
            }

            public Pattern Next()
            {
                pattern++;
                
                if(this[0] == 1)
                {
                    trainNum++;
                    //  if(trainNum > k) this[0] = 3; // k本を超えたらその1の桁は探索スキップするため繰り上げる
                }
                for (int i = 0; i < N ; i++)
                {
                    // if(this[i] == 1 && trainNum > k) this[i]=3; // 桁スキップ
                    // if(pattern[i] <= 2) // 繰り上がらない
                    // {
                    //     // if( trainNum > k) // k本を超えたらその桁は探索スキップ
                    //     // {
                    //     //     pattern[i] = 3;
                    //     // }
                    // }
                    if(this[i] > 2) // 繰り上がる
                    {
                        this[i] = 0; trainNum--;
                        this[i+1]++;
                        if(this[i+1] == 1) trainNum++;
                        if(this[N] == 1) {return null;}
                    }
                }
                // WriteLine("pattern: " + Convert.ToString(pattern, 2) + "   train: " + trainNum);
                return this;
            }

            public static IEnumerable<long> Comobination(long pattern, int k, int step, int rightInit, int N)
            {
                // WriteLine($"p: {Convert.ToString(pattern,2).PadLeft(N*2, '0')} k: {k}  step:{step}");
                if (step >= k)
                {
                    yield return pattern;
                    yield break;
                }
                for (int right = rightInit; right <= N - k + step; right++)
                {
                    pattern = pattern | ((long)1 << (2 * right));
                    
                    foreach (var newPattern in Comobination(pattern, k, step+1, right + 1, N))
                    {
                        yield return newPattern;
                    }
                    pattern = pattern & ~(1 << (2 * right));
                    pattern = pattern | ((long)1 << (2 * right + 1));
                    foreach (var newPattern in Comobination(pattern, k, step+1, right + 1, N))
                    {
                        yield return newPattern;
                    }
                    pattern = pattern & ~(1 << (2 * right + 1));
                }

            }
         

            public long this[int i]
            {
                get { return (pattern >> (i*2)) & 0b11;}
                set { pattern = (pattern & ~(0b11 << (i*2))) | value << (i*2);}
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
