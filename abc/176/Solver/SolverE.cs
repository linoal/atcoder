using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace abc176
{
    class SolverE
    {
        static void Main()
        {
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        
        long H,W,M;
        long[] ht, wt;


        public void Solve()
        {
            checked
            {
                // Debug.Put(2,2); Debug.Put('A',2); Debug.Put("strtest",2);
                // Debug.Put(new int[]{1,2,3},2);
                // Debug.Put(2,"num", new int[]{1,2,3}, "intarr");
                // Debug.Put(new int[,]{{1,2,3},{4,5,6},{7,8,9}}, "2-dim array",2);
                (H,W,M) = Get.Tuple<long,long,long>();
                ht = new long[M]; wt = new long[M];
                
                for(int i=0; i<M; i++){
                    (ht[i], wt[i]) = Get.Tuple<long,long>();
                    ht[i]--; wt[i]--;
                }

                long[] hn = new long [H];
                long[] wn = new long [W];
                long maxHn=0, maxWn=0, hl=0, wl=0;
                for(int m=0; m<M; m++){
                    long h = ht[m]; long w = wt[m];
                    hn[h]++; wn[w]++;
                    if(maxHn < hn[h]){
                        maxHn = hn[h];
                        hl=1;
                    }else if(maxHn == hn[h]){
                        hl++;
                    }
                    if(maxWn < wn[w]){
                        maxWn = wn[w];
                        wl = 1;
                    }else if(maxWn == wn[w]){
                        wl++;
                    }
                }
                long b=0;
                for(int m=0; m<M; m++){
                    if(hn[ht[m]] == maxHn && wn[wt[m]] == maxWn){
                         b++;
                    }
                }
                
                long ans = maxHn + maxWn;
                ans = b == hl * wl ? ans-1 : ans;

                Debug.Put(hn,"hn",wn,"wn");
                Debug.Put(hl,"hl",wl,"wl");
                Debug.Put(new int[,]{{1,2,3},{4,5,6}}, "arr");
                WriteLine(ans);
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

            // Put、ラベル付き版
            public static void Put(object obj, string label, int padLeft = 0, bool newline = true){
                Write("\u001b[32m{0}:\u001b[0m ", label);
                Put(obj, padLeft, newline);
            }

            // Put、オブジェクトとラベルを複数渡す版
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
