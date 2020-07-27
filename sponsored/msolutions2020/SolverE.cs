using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;


namespace msolutions2020
{
    class SolverE
    {
        Dictionary<ulong,long>[] costX;
        Dictionary<ulong,long>[] costY;
        static int N;
        static Village[] V;
        static void Main()
        {
            new SolverE().Solve();
        }

        public void Solve()
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
            long pow2_N = IntPow(2,(uint)N);
            costX = new Dictionary<ulong,long>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                costX[i] = new Dictionary<ulong,long>(60000);
                
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnX = 0; ptnX < pow2_N; ptnX++)
                {
                    ulong ptnKey = 0;
                    for(int i=0; i<N; i++)
                    {
                        ptnKey = (ptnX >> i & 1)==1 ? (ptnKey | ((ulong)1 << 2*i)) : ptnKey;
                    }
                    // WriteLine($"x: ptnKey={Convert.ToString(ptnKey,2)}");
                    costX[vil].Add(ptnKey, Dim1Pattern.DistX(ptnX, V[vil], V) * V[vil].p);
                }
            }

            costY = new Dictionary<ulong,long>[V.Length];
            for(int i=0; i<V.Length; i++)
            {
                costY[i] = new Dictionary<ulong,long>(60000);
            }
            for(int vil=0 ; vil < V.Length ; vil++)
            {
                for(int ptnY = 0; ptnY < pow2_N; ptnY++)
                {
                    ulong ptnKey = 0;
                    for(int i=0; i<N; i++)
                    {
                        ptnKey = (ptnY >> i & 1)==1 ? (ptnKey | ((ulong)1 << 2*i+1)) : ptnKey;
                    }
                    // WriteLine($"y: ptnKey={Convert.ToString(ptnKey,2)}");
                    costY[vil].Add(ptnKey, Dim1Pattern.DistY(ptnY, V[vil], V) * V[vil].p);
                }
            }

            //WriteLine("dict gen.");

            // 事前辞書の構築 ここまで


            Pattern pattern = new Pattern(N);
            long[] ans = Repeat(long.MaxValue, N+1).ToArray();
            // long ptnNum = 0; //debug
            do
            {
                // WriteLine($"pattern: {Convert.ToString(pattern.pattern,2).PadLeft(N*2, '0')}  train: {pattern.trainNum}");
                int t = BitOperations.PopCount(pattern.pattern);
                ans[t] = Min(ans[t], cost(pattern.pattern));

                // ptnNum++; //debug
            }while(pattern.Next() != null);
            // WriteLine($"ptnnum: {ptnNum}");

            foreach(var a in ans)
            {
                WriteLine(a);
            }
                      
        }

        const long xmask = 0b_01010101010101010101010101010101;
        const long ymask = 0b_10101010101010101010101010101010;
        long cost(ulong ptn)
        {

            int vLength = V.Length;
            long cost = 0;
            
            for (int v = 0; v < vLength; v++)
            {
                if( ((ptn >> (v * 2)) & 0b11) > 0 ) continue;
                cost += Min(costX[v][ptn & xmask], costY[v][ptn & ymask]);
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
            public ulong pattern;
            // public int trainNum = 0;
            int N;

            public Pattern(int _n)
            {
                pattern = 0;
                N = _n;
            }

            public Pattern Next()
            {
                pattern++;
                
                // if(/*this[0]*/ (pattern & 0b11) == 1)
                // {
                //     trainNum++;
                // }
                for (int i = 0; i < N ; i++)
                {

                    if(this[i] > 2) // 繰り上がる
                    {
                        this[i] = 0;// trainNum--;
                        this[i+1]++;
                        // if(this[i+1] == 1) trainNum++;
                        
                    }else break; // 繰り上がらない

                    if(this[N] == 1) {return null;} // 終了
                }
                // WriteLine("pattern: " + Convert.ToString(pattern, 2) + "   train: " + trainNum);
                return this;
            }
         

            public ulong this[int i]
            {
                get { return (pattern >> (i*2)) & 0b11;}
                set { pattern = (pattern & (ulong)~(0b11 << (i*2))) | value << (i*2);}
            }
        }
  

        class Dim1Pattern
        {

            public static long DistX(int pattern, Village v, Village [] Vils)
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

            public static long DistY(int pattern, Village v, Village [] Vils)
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

        static long IntPow(int x, uint pow)
        {
            long ret = 1;
            while ( pow != 0 )
            {
                if ( (pow & 1) == 1 )
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
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
