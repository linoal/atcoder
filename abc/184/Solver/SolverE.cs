using System.Globalization;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ABC184.SolverEExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ABC184{
    class SolverE{
        static void Main(){
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverE().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                
                (var h, var w) = Get.Tuple<int,int>();
                

                // (var sy, var sx) = Get.Tuple<int,int>();
                // (var gy, var gx) = Get.Tuple<int,int>();

                var m = Get.Char2d(h,w);

                // m[--sy,--sx] = 'S'; m[--gy,--gx] = 'G';
                

                int SY = -1, SX = -1;
                for(int i=0; i<h; i++){
                    for(int j=0; j<w; j++){
                        if(m[i,j]=='S'){
                            (SY, SX) = (i,j);
                            break;
                        }
                    }
                }

                var bfs = new BFS(m);
                bfs.isDebugMode = true;
                var cost = bfs.Search(SY,SX);
                WriteLine(cost);
                


            }
        }

        class BFS{
            /**
            使い方例：
                var bfs = new BFS(charMap);
                bfs.isDebugMode = false;
                var cost = bfs.Search(SY,SX);
                WriteLine(cost);
            **/
            public char[,] map;
            public bool isDebugMode = false;
            long[,] costs;
            Queue<Node> que = new Queue<Node>();
            int H; int W;
            bool[] portalsUsed = new bool[26];

            public BFS(char[,] _map){
                map = _map;
                H = map.GetLength(0);
                W = map.GetLength(1);
                costs = new long[H,W];
                for(int i=0; i<H; i++){
                    for(int j=0; j<W; j++){
                        costs[i,j] = long.MaxValue;
                    }
                }

                for(int i=0; i<portalsUsed.Length; i++){
                    portalsUsed[i] = false;
                }
            }

            public long Search(int sy, int sx){ // sx,sy はスタート地点座標
                costs[sy,sx] = 0;
                que.Enqueue(new Node(sy,sx));
                while(que.Count != 0){
                    var node = que.Dequeue();
                    var y = node.y; var x = node.x;
                    long currentCost = costs[y,x];
                    // ゴール条件
                    if(map[y,x]=='G'){
                        return currentCost;
                    }
                    // 各方向
                    for(int i=-1; i<=1; i++){
                        for(int j=-1; j<=1; j++){
                            // 探索条件
                            if(i==0 && j==0 && map[y,x] >= 'a' && map[y,x] <= 'z'){
                                int pn = map[y,x] - 'a';
                                if(portalsUsed[pn] == false){
                                    portalsUsed[pn] = true;
                                    for(int ph=0; ph<H; ph++){
                                        for(int pw=0; pw<W; pw++){
                                            if(ph==y && pw==x) continue;
                                            if(map[ph,pw]==map[y,x]){
                                                costs[ph,pw] = Min(costs[y,x]+1, costs[ph,pw]);
                                                que.Enqueue(new Node(ph,pw));
                                            }
                                        }
                                    }
                                }
                            }
                            if(Abs(i)+Abs(j) != 1) continue; // 探索対象のマンハッタン距離
                            int ty = y+i; int tx = x+j; // target x,y
                            if(ty<0 || ty >= H || tx<0 || tx >=W) continue;
                            var tc = map[y+i, x+j]; // target char
                            if(tc != '.' && tc!='G' && (tc<'a' || tc>'z')) continue; // 探索対象のタイル
                            if(costs[ty,tx] <= currentCost+1) continue; // 探索済みか?
                            costs[ty,tx] = currentCost+1;
                            que.Enqueue(new Node(ty,tx));

                        }
                    }
                    if(isDebugMode){
                        DebugPut();
                        ReadLine();
                    }
                    
                }
                // ゴール条件を満たさなかった場合
                return -1;
            }

            public void DebugPut(){
                var strs = new string[H,W];
                for(int i=0; i<H; i++){ // 各マスの文字列化
                    for(int j=0; j<W; j++){
                        char tile = map[i,j];
                        long costLong = costs[i,j];
                        string costStr = ( costLong == long.MaxValue ) ? "" : costLong.ToString(); 
                        strs[i,j] = $"{Green(tile.ToString())}{Cyan(costStr.PadRight(2))}";
                    }
                }
                // 列番号の出力
                Write("   ");
                for(int i=0; i<W; i++){
                    Write(Gray($"{i.ToString().PadRight(4)}"));
                }
                Write("\n");
                // 各マスと行番号の出力
                for(int i=0; i<H; i++){ 
                    Write(Gray($"{i.ToString().PadLeft(2)}|"));
                    for(int j=0; j<W; j++){
                        Write(strs[i,j]);
                        Write(Gray("|"));
                    }
                    Write("\n");
                }

                // キューの出力
                Write(Gray("queue(y,x) : "));
                var queArr = que.ToArray();
                for(int i=0; i<queArr.Length; i++){
                    Write(Cyan(queArr[i].ToString()));
                }
                Write("\n");
            }

            static string Bold(string str){ return $"\u001b[1m{str}\u001b[0m"; }
            static string Cyan(string str){ return $"\u001b[96m{str}\u001b[0m"; }
            static string Green(string str){ return $"\u001b[92m{str}\u001b[0m"; }
            static string Gray(string str){ return $"\u001b[90m{str}\u001b[0m"; }


            struct Node{
                public int y; public int x;
                public Node(int _y, int _x){
                    y = _y; x = _x;
                }
                public override string ToString()
                {
                    return $"({y},{x})";
                }
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
            public static char[,] Char2d(int H, int W){
                var ret = new char[H,W];
                for(int i=0; i<H; i++){
                    var line = Get.Str().ToCharArray();
                    // Debug.Put(line,"line",H,"H",W,"W");
                    for(int j=0; j<W; j++){
                        ret[i,j] = line[j];
                    }
                }
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
