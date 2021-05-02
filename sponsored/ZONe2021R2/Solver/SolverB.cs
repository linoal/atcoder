using System.Text.RegularExpressions;
using System.Resources;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ZONe2021R2.SolverBExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ZONe2021R2{
    class SolverB{

        static void Main(){
            Debug.isDebugMode = false;
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverB().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                string inputStr = @"2 3 2 5 6 6 7 4 6 2 4 4 14 8 1 9 15 17 5 19
2 2 2 7 7 1 8 6 15 11 4 15 17 26 9 7 21 4 13 10
4 3 2 1 16 4 8 15 26 9 19 11 31 29 9 38 24 27 21 34
3 6 12 8 1 4 23 6 7 26 38 47 36 19 45 28 40 33 9 19
3 5 1 15 5 3 10 20 6 19 15 33 19 27 41 12 66 59 33 35
7 10 10 13 30 2 42 3 55 54 4 51 67 39 59 10 30 2 25 73
4 6 14 16 2 5 48 53 45 62 32 19 26 18 95 63 112 99 112 48
6 5 10 10 11 26 24 55 64 30 65 87 57 60 110 28 127 17 50 78
7 2 12 1 40 22 5 27 58 16 31 15 92 63 128 5 95 159 100 161
6 16 23 28 13 14 5 30 40 70 95 6 32 58 109 138 31 131 110 106
2 18 12 42 40 35 72 48 33 12 112 63 122 9 118 115 187 17 164 34
2 20 32 20 58 56 65 95 80 112 123 24 100 110 1 125 29 70 150 211
7 17 30 10 63 69 43 41 5 84 47 124 21 60 109 65 37 97 57 258
5 20 36 30 64 62 95 112 84 93 6 68 8 106 138 69 197 133 133 106
14 16 7 53 28 47 52 55 7 38 143 17 62 104 61 199 235 124 166 280
16 3 36 51 16 38 45 46 9 24 75 87 94 127 73 50 229 38 132 234
4 12 27 25 46 61 118 55 145 80 157 129 158 218 106 185 286 64 45 147
5 30 13 12 24 25 52 84 157 131 78 177 89 158 96 121 293 306 324 188
9 2 7 8 52 78 107 148 49 105 192 104 116 252 202 96 227 258 211 367
12 10 41 48 4 87 107 102 28 143 24 99 8 234 171 34 72 320 33 295";
                
                var s = Regex.Split(inputStr, @"\n|\s").Where(s => s != "");
                Debug.Put(s, "s");
                var ns = s.Select(s => Convert.ToInt32(s)).ToArray();
                int ans = 0;
                foreach(var n in ns){
                    bool isPrime = true;
                    if(n<=1) continue;
                    for(int i=2; i*i<=n; i++){
                        if(n % i == 0){
                            isPrime = false;
                            break;
                        }
                    }
                    if(isPrime){
                        ans++;
                        Debug.Put($"{n} is prime");
                    }
                }
                WriteLine(ans);
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
                }else if( obj is System.Collections.IEnumerable ie){
                    Write(Green("{ "));
                    foreach(var e in ie){ Put(e,0,false); Write(Green(",")); }
                    Write(Green(" }"));
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
            public static char[,] CharMap(int H, int W){
                var map = new char[H,W];
                for(int i=0; i<H; i++){
                    string line = Get.Str();
                    for(int j=0; j<W; j++){
                        map[i,j] = line[j];
                    }
                }
                return map;
            }
        }
    }
     // 同じ拡張メソッドは同一namespace内で定義できないのでnamespaceを問題ごとに分ける
    namespace SolverBExtensions{
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
