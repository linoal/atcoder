using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ABC181.SolverEExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC181{
    class SolverE{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                (int N, int M) = Get.Tuple<int,int>();
                long[] H = Get.Longs();
                long[] W = Get.Longs();
                Array.Sort(H); Array.Sort(W);

                long[] sumA = new long[N/2+1];
                long[] sumB = new long[N/2+1];
                sumA[0] = 0; sumB[0] = 0;
                for(int i=1; i*2<N; i++){
                    sumA[i] = sumA[i-1] + Abs(H[2*i-1] - H[2*i-2]);
                    sumB[i] = sumB[i-1] + Abs(H[2*i] - H[2*i-1]);
                }

                // Debug.Put(H,"H");


                // Debug.Put(sumA,"sumA"); Debug.Put(sumB,"sumB");

                long minDif = long.MaxValue;

                // int teacherIndexPrev = -1;
                for(int i=0; i<M; i++){
                    // int teacherIndex = N-1;
                    // for(int j=Max(teacherIndexPrev,0); j<N-1; j++){
                    //     if(W[i]<H[j]){
                    //         teacherIndex = j-1;
                    //         break;
                    //     }
                        
                    // }
                    // // Debug.Put(W[i], "teacher height");
                    // if(W[i] <= H[0]){
                    //     teacherIndex = -1;
                    // }

                    // teacherIndexPrev = teacherIndex;
                    int teacherIndex = H.LowerBound(W[i])-1;
                    // {
                    //     int l=0; int r=N-1; int c = (l+r)/2;
                    //     while(l<r){
                            
                    //         c = l + (r-l)/2;
                    //         // Debug.Put(l,"l",r,"r",c,"c"); ReadLine();
                    //         if(H[c]<W[i]) l = c+1;
                    //         else r = c;
                            
                    //     }
                    //     teacherIndex = r-1;
                    //     if(teacherIndex == N-2){
                    //         if(H[N-1] <= W[i]){
                    //             teacherIndex = N-1;
                    //         }
                    //     }
                    //     // Debug.Put(teacherIndex,"teacherIndex");
                    // }



                    // Debug.Put(teacherIndex,"teacherIndex");

                    long diff = 0;
                    diff += sumA[(teacherIndex+1)/2];
                    diff += sumB[N/2];
                    diff -= sumB[(teacherIndex+1)/2];
                    if(teacherIndex%2==1 || teacherIndex==-1){
                        diff += Abs(W[i] - H[teacherIndex+1]);
                    }else{
                        diff += Abs(W[i] - H[teacherIndex]);
                    }
                    minDif = Min(minDif, diff);
                }
                WriteLine(minDif);



            }
        }




        // === ここからライブラリ
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
            public static (T,U,V,W) Tuple<T,U,V,W>() {string[] strs = Strs(); T t = TypeConv<T>(strs[0]); U u = TypeConv<U>(strs[1]); V v = TypeConv<V>(strs[2]); W w = TypeConv<W>(strs[3]); return(t,u,v,w);}
            public static T[] Lines<T>(int N){
                T[] ret = new T[N];
                for(int i=0; i<N; i++){ ret[i] = TypeConv<T>(Str()); }
                return ret;
            }
            
        }
    }
     // 同じ拡張メソッドは同一namespace内で定義できないのでnamespaceを問題ごとに分ける
    namespace SolverEExtensions{
        static class ArrayExtensions{
            public static T[] Fill<T>(this T[] arr, T val){
                for(int i=0; i<arr.Length; i++){
                    arr[i] = val;
                }
                return arr;
            }
            public static T[,] Fill<T>(this T[,] arr, T val ){
                for(int i=0; i<arr.GetLength(0); i++){
                    for(int j=0; j<arr.GetLength(1); j++){
                        arr[i,j] = val;
                    }
                }
                return arr;
            }

            public static int LowerBound<T>(this T[] array, T val) where T: struct, IComparable<T>{
                int l = 0;
                int r = array.Length - 1;
                while(l<=r){
                    int mid = l + (r-l)/2;
                    if(array[mid].CompareTo(val) < 0) l = mid + 1;
                    else r = mid - 1;
                }
                return l;
            }
        }
    }
}
