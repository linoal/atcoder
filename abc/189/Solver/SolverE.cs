using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ABC189.SolverEExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC189{
    class SolverE{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                int N = Get.Int();
                var X = new long[N];
                var Y = new long[N];
                for(int i=0; i<N; i++){
                    (X[i], Y[i]) = Get.Tuple<int,int>();
                    
                }
                int M = Get.Int();
                Matrix[] affines = new Matrix[M+1];
                for(int i=0; i<M+1; i++){
                    affines[i] = new Matrix(3,3);
                }
                affines[0] = new Matrix(new long[,] {{1,0,0},{0,1,0},{0,0,1}});

                Matrix op1 = new Matrix(new long[,] {{0,1,0},{-1,0,0},{0,0,1}});
                Matrix op2 = new Matrix(new long[,] {{0,-1,0},{1,0,0},{0,0,1}});
                StringBuilder answer = new StringBuilder();

                for(int m=1; m<=M; m++){
                    string[] op = Get.Strs();
                    Matrix opAffine;
                    if(op[0] == "1"){
                        opAffine = op1;
                    }else if(op[0] == "2"){
                        opAffine = op2;
                    }else if(op[0] == "3"){
                        long p = Int32.Parse(op[1]);
                        opAffine = new Matrix(new long[,] {{-1,0,2*p},{0,1,0},{0,0,1}});
                    }else{
                        long p = Int32.Parse(op[1]);
                        opAffine = new Matrix(new long[,] {{1,0,0},{0,-1,2*p},{0,0,1}});
                    }
                    affines[m] = opAffine * affines[m-1];
                    // Debug.Put(affines[m], $"affiens[{m}]");
                }
                int Q = Get.Int();
                for(int q=0; q<Q; q++){
                    (var a, var b) = Get.Tuple<int,int>();
                    Matrix r = new Matrix(new long[,] {{X[b-1]},{Y[b-1]},{1}});
                    Matrix l = affines[a];
                    // Debug.Put(r,"r",l,"l");
                    Matrix ans = l*r;
                    answer.Append($"{ans[0,0]} {ans[1,0]}\n");
                }

                WriteLine(answer.ToString());
                // Sim sim = new Sim(N,M);
                // sim.Init(X,Y);

                // int Q = Get.Int();
                // for(int i=0; i<Q; i++){
                //     (int a, int b) = Get.Tuple<int,int>();
                //     sim.Query(a,b);
                // }




            }
        }

        class Matrix{
            long[,] mat;
            public int RowNum{
                get{ return mat.GetLength(0);}
            }
            public int ColNum{
                get{ return mat.GetLength(1);}
            }
            public Matrix(int rowSize, int colSize){
                mat = new long[rowSize, colSize];
            }
            public Matrix(long[,] arg_mat){
                mat = arg_mat;
            }

            public long this[int i, int j]{
                set{
                    mat[i,j] = value;
                }
                get{
                    return mat[i,j];
                }
            }

            public static Matrix operator* (Matrix a, Matrix b){
                if(a.ColNum != b.RowNum){
                    throw new ArithmeticException("行の数と列の数が合わないため、積がありません。");
                }
                // Debug.Put(a,"a",b,"b");
                Matrix prod = new Matrix(a.RowNum, b.ColNum);
                for(int i=0; i<prod.RowNum; i++){
                    for(int j=0; j<prod.ColNum; j++){
                        long sum = 0;
                        for(int k=0; k<a.ColNum; k++){
                            sum += a[i,k] * b[k,j];
                        }
                        prod[i,j] = sum;
                    }
                }
                return prod;
            }

            public override string ToString()
            {
                int maxStrLen = 0; // 整形用に文字数チェック
                for(int i=0; i<RowNum; i++){
                    for(int j=0; j<ColNum; j++){
                        maxStrLen = Max(maxStrLen, this[i,j].ToString().Length);
                    }
                }
                string ret = "\n";
                for(int i=0; i<RowNum; i++){
                    ret += "| ";
                    for(int j=0; j<ColNum; j++){
                        ret += $"{this[i,j].ToString().PadLeft(maxStrLen)}";
                        if(j == ColNum-1){ ret += " "; }
                        else{ ret += ", "; }
                    }
                    ret += "|\n";
                }
                return ret;
            }
        }

        // struct Coord{
        //     public long x; public long y;
        //     public Coord(long _x, long _y){
        //         x = _x; y = _y;
        //     }
        // }

        // class Chessman{
        //     public Coord[] coords;
        //     public Chessman(int coordRecNum){
        //         coords = new Coord[coordRecNum];
        //     }
        //     public Coord current;
        // }

        // // struct Op{
        // //     int type;
        // //     int operand;
        // // }

        // class Sim{
        //     Chessman[] chessmans;
        //     // Op[] ops;
        //     int N;
        //     int M;
        //     string[] query;
        //     int recInterval = 100;
        //     int turn = 0;
        //     public Sim(int _N, int _M_){
        //         N = _N;
        //         M = _M_;
        //         chessmans = new Chessman[N];
        //         query = new string[M];
        //         for(int i=0; i<N; i++){
        //             chessmans[i] = new Chessman(M / recInterval);
        //         }
        //         // ops = new Op[M];
        //     }

        //     public void Init(long[] X, long[] Y){
        //         for(int i=0; i<N; i++){
        //             chessmans[i].coords[0] = new Coord(X[i], Y[i]);
        //             chessmans[i].current = new Coord(X[i], Y[i]);
        //         }
        //     }

        //     public void Do(string m){
        //         foreach(var man in chessmans){
        //            man.current = Calc(man.current, m);
        //        }
        //        query[turn] = m;
        //         turn++;
        //         if(turn % recInterval == 0){
        //             foreach(var man in chessmans){
        //                 man.coords[turn/recInterval] = man.current;
        //             }
        //         }
        //     }

        //     public static Coord Calc(Coord current, string query){
        //         string[] order = query.Split(" ");
        //         Coord next = new Coord(0,0);
        //         if(order[0] == "1"){
        //             (next.x, next.y) = (current.y, current.x * -1);
        //         }
        //         if(order[0] == "2"){
        //             (next.x, next.y) = (current.y * -1, current.x);
        //         }
        //         if(order[0] == "3"){
        //             int p = Int32.Parse(order[1]);
        //             next.x = current.x  * -1 + 2 * p; 
        //         }
        //         if(order[0] == "4"){
        //             int p = Int32.Parse(order[1]);
        //             next.y = current.y * -1 + 2 * p;
        //         }
        //         return next;
        //     }

        //     public void Query(int A, int B){
        //         Coord current = chessmans[B-1].coords[A/recInterval];
        //         for(int i=A%recInterval; i<A; i++){
        //             current = Calc(current, query[i]);
        //         }
        //         WriteLine($"{current.x} {current.y}");
        //     }
        // }


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
        
        static class Debug{ // Debug用の出力は、各行に色付きの部分が必要。でないとTesterが本出力とDebug用出力の見分けが付かずに誤判定する。
            public static bool isDebugMode = true;
            public static void Put(object obj, int padLeft = 0, bool newline = true){
                if (!isDebugMode) return;
                if (obj is Array arr){
                    if( arr.Rank == 1 ){
                        Write(Green("["));
                        for(int i=0; i<arr.Length; i++){
                            Put(arr.GetValue(i), padLeft, false);
                            if( i < arr.Length - 1 ) Write(", ");
                        }
                        Write(Green("]"));
                    }
                    else if( arr.Rank == 2 ){
                        Write(Green("[\n"));
                        for(int i=0; i<arr.GetLength(0); i++){
                            Write(Green(" ["));
                            for(int j=0; j<arr.GetLength(1); j++){
                                Put(arr.GetValue(i,j), padLeft, false);
                                if (j < arr.GetLength(1) - 1) Write(Green(", "));
                            }
                            if (i < arr.GetLength(0) - 1 ) Write(Green("],\n"));
                            else Write(Green("]\n"));
                        }
                        Write(Green("]"));
                    }
                }
                else if( obj is ValueType val ){
                    Put(val.ToString(), padLeft, newline);
                }
                else if( obj is string str ){
                    Write(Green(str.PadLeft(padLeft)));
                }else if( obj is Dictionary<int,int> dic){
                    Write(Green($"dicionary: "));
                    foreach(var pair in dic){
                        Write(Green($"{pair.Key}=>{pair.Value}, "));
                    }
                }
                else{
                    Write(Green(obj.ToString().PadLeft(padLeft)));
                }

                if(newline) Write("\n");
            }

            public static void Put(object obj, string label, int padLeft = 0, bool newline = true){
                if (!isDebugMode) return;
                Write(Bold(Green(label + ": ")));
                Put(obj, padLeft, newline);
            }
            public static void Put(params object[] args){
                if (!isDebugMode) return;
                if (args.Length % 2 != 0){
                    WriteLine("Debug.Put(params): arg length shall be multiple of 2.");
                }
                for(int i=0; i<args.Length; i += 2){
                    Put(args[i], (string)args[i+1],0, false);
                    Write("   ");
                }
                Write("\n");
            }

            static string Green(string str){
                return "\u001b[32m" + str + "\u001b[0m";
            }
            static string Bold(string str){
                return "\u001b[1m" + str + "\u001b[0m";
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
                int len0 = arr.GetLength(0);
                int len1 = arr.GetLength(1);
                for(int i=0; i<len0; i++){
                    for(int j=0; j<len1; j++){
                        arr[i,j] = val;
                    }
                }
                return arr;
            }

            // 昇順の配列について、指定の値以上の値を持つ最小のインデックスを返す。
            // すべて指定の値未満である場合は、配列の最後のインデックス+1(=array.Length)が返る。
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
