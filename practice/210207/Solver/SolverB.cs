using System.Dynamic;
using System.Resources;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Practice210207.SolverBExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace Practice210207{
    class SolverB{

        static void Main(){
            Debug.isDebugMode = false;
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverB().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                (var N, var M, var T) = Get.Tuple<int,int,long>();
                var As = Get.Longs();
                var g = new Graph(N,M);
                var gInv = new Graph(N,M);
                for(int m=0; m<M; m++){
                    (var a, var b, var c) = Get.Tuple<int,int,long>();
                    g.AddEdge(a-1,b-1,c);
                    gInv.AddEdge(b-1,a-1,c);
                }
                // Debug.Put(g, "g");
                var costs0toI = g.Dijkstra(0);
                var costsIto0 = gInv.Dijkstra(0);

                long maxReward = 0;
                for(int i=0; i<N; i++){
                    if(costs0toI[i] == long.MaxValue || costsIto0[i] == long.MaxValue) continue;
                    maxReward = Max(maxReward, As[i] * (T - costs0toI[i] - costsIto0[i]));
                }
                WriteLine(maxReward);


            }
        }

        // グラフ。隣接リストの構造で表現。
        // 注意 : 別途PriorityQueueも必要。
        // 辺は有向としているので、無向のときは双方向にAddEdges()すること。
        // ダイクストラ探索を実装。
        // 使い方例 :
        // var g = new Graph(N,M);
        // g.AddEdge(a-1,b-1,cost); // これを繰り返してグラフ構築
        // long[] costs = g.Dijkstra(startId);
        public class Graph{
            public Vert[] verts; // 各Vertは、そこから出るEdgeのリストを持っている。
            public List<Edge> edges; // 各Edgeはfrom,to,costを持っている

            public Graph(int vertsNum, int edgesCapacity){
                verts = new Vert[vertsNum];
                edges = new List<Edge>(edgesCapacity);
                for(int i=0; i<vertsNum; i++){
                    verts[i] = new Vert(i);
                }
            }


            public class Vert{
                public int id; // このidはGraphのverts[]のindexと一致することが前提。
                public List<Edge> adjEdges; // この頂点から出ている道。来る道ではない。
                public Vert(int argId){
                    adjEdges = new List<Edge>();
                    id = argId;
                }
                public void AddAdjEdge(Edge edge){
                    adjEdges.Add(edge);
                }
                
            }
            public class Edge{
                public Vert from, to;
                public long cost; // その辺の重み
                public Edge(Vert argFrom, Vert argTo, long argCost){
                    (from, to, cost) = (argFrom, argTo, argCost);
                }
                public override string ToString(){
                    return $"(to {to.id}, cost {cost})";
                }
            }

            // ダイクストラで探索する用の頂点データ構造
            class VertForDijkstra : IComparable<VertForDijkstra>{
                public int id;
                public long cost;
                public int CompareTo(VertForDijkstra other){
                    // コストの小さいほうから処理したいのでそのように並び順を定義する
                    return cost.CompareTo(other.cost) * -1;
                }
            }

            public void AddEdge(int idFrom, int idTo, long cost){
                if(idFrom >= verts.Length || idTo >= verts.Length){
                    throw new IndexOutOfRangeException($"[[ vertのindexは0から{verts.Length-1}までですが、範囲外のindexが渡されました。idFrom:{idFrom}, idTo:{idTo} ]]");
                }
                Edge newEdge = new Edge(verts[idFrom], verts[idTo], cost);
                edges.Add(newEdge);
                verts[idFrom].AddAdjEdge(newEdge);
            }

            // ダイクストラ法により最短経路のコストを求める。
            // 注意 : 経路がない場合は ret[i] = long.MaxValue になる。
            // 戻り値 : ret[i] = 始点から頂点iまでの最短経路のコスト
            public long[] Dijkstra(int startVertId){
                // 初期化
                var costs = new long[verts.Length];
                for(int i=0; i<costs.Length; i++){
                    costs[i] = long.MaxValue;
                }
                costs[startVertId] = 0;
                var que = new PriorityQueue<VertForDijkstra>(verts.Length);
                que.Push(new VertForDijkstra{id = startVertId, cost = 0});
                // 探索
                while(que.HasValue){
                    var vert = que.Pop();
                    var id = vert.id;
                    // 記録されているコストと異なるときは無視（Queueにいる間により効率的な経路が見つかったということ）
                    if(vert.cost != costs[id]) continue;
                    // 注目頂点から繋がっている箇所を見る
                    foreach(var edge in verts[id].adjEdges){
                        var dstId = edge.to.id;
                        long dstCost = costs[id] + edge.cost;
                        if(dstCost < costs[dstId]){
                            costs[dstId] = dstCost;
                            que.Push(new VertForDijkstra{id = dstId, cost = dstCost});
                        }
                    }
                }
                return costs;
            }

            public override string ToString(){
                var sb = new StringBuilder();
                sb.Append("Graph :\n");
                sb.Append("    Verts :\n");
                for(int i=0; i<verts.Length; i++){
                    sb.Append($"        {i} : Edges : {{ ");
                    foreach(var edge in verts[i].adjEdges){
                        sb.Append($"{edge} , ");
                    }
                    sb.Remove(sb.Length-3,2); // 最後の", "を削除
                    sb.Append("}\n");
                }
                return sb.ToString();
            }
        }



        public class PriorityQueue<T> where T : IComparable<T>{
            List<T> heap;
            public bool IsEmpty{get{return heap.Count==0;}}
            public bool HasValue{get{return !IsEmpty;}}

            public PriorityQueue(int capacity){
                heap = new List<T>(capacity);
            }
            
            public void Push(T elem){
                int current = heap.Count;
                heap.Add(elem);
                while(current != 0){
                    int parent = (current - 1) / 2;
                    if(heap[current].CompareTo(heap[parent]) <= 0){
                        break;
                    }
                    // 親と値を入れ替え
                    SwapIndex(current, parent);
                    current = parent;
                }
            }

            public T Peek(){
                if(heap.Count <= 0) throw new ArgumentOutOfRangeException("ERROR : PriorityQueue : no element to peek");
                return heap[0];
            }

            public T Pop(){
                if(heap.Count <= 0) throw new ArgumentOutOfRangeException("ERROR : PriorityQueue : no element to pop");
                T pop = Peek();
                int lastIndex = heap.Count-1;
                heap[0] = heap[lastIndex];
                heap.RemoveAt(lastIndex--);
                int parent = 0;
                while(true){
                    int childLeft = parent * 2 + 1;
                    if(childLeft > lastIndex) break;
                    int childRight = childLeft + 1;
                    int childBigger = childLeft;
                    if(childRight <= lastIndex && heap[childRight].CompareTo(heap[childLeft]) > 0){
                        childBigger = childRight;
                    }
                    if(heap[childBigger].CompareTo(heap[parent]) < 0) break;
                    SwapIndex(childBigger, parent);
                    parent = childBigger;
                }
                return pop;
            }


            public override string ToString(){
                if(heap.Count <= 0) return "Empty primary queue";
                var sb = new StringBuilder();
                sb.Append($"(top: {Peek()} , other : ");
                for(int i=1; i<heap.Count; i++){
                    sb.Append($"{heap[i]}");
                    if(i < heap.Count-1){
                        sb.Append(", ");
                    }
                }
                sb.Append(" )");
                return sb.ToString();
            }

            private void SwapIndex(int indexA, int indexB){
                T tmp = heap[indexA];
                heap[indexA] = heap[indexB];
                heap[indexB] = tmp;
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

        static class Get{
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
