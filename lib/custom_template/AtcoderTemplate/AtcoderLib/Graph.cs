using System;
using System.Collections.Generic;
using System.Text;

namespace AtcoderLib{
    // 注意 : PriorityQueueが必要
    // 機能 : new GraphしてAddEdgeでグラフ表現。経路コストはDijkstraで求める。
    public class Graph{
        public Node[] nodes; // 各Nodeは、そこから出るEdgeのリストを持っている。
        public List<Edge> edges; // 各Edgeはfrom,to,costを持っている

        public Graph(int nodesNum, int edgesCapacity){
            nodes = new Node[nodesNum];
            edges = new List<Edge>(edgesCapacity);
            for(int i=0; i<nodesNum; i++){
                nodes[i] = new Node(i);
            }
        }

        // サブクラス ここから
        public class Node{
            public int id; // このidはGraphのnodes[]のindexと一致することが前提。
            public List<Edge> adjEdges; // この頂点から出ている道。来る道ではない。
            public Node(int argId){
                adjEdges = new List<Edge>();
                id = argId;
            }
            public void AddAdjEdge(Edge edge){
                adjEdges.Add(edge);
            }

            // 隣接ノードを返す
            public List<Node>AdjNodes(){
                var adjNodes = new List<Node>();
                foreach(var edge in adjEdges){
                    adjNodes.Add(edge.to);
                }
                return adjNodes;
            }
            
        }
        public class Edge{
            public Node from, to;
            public long cost; // その辺の重み
            public Edge(Node argFrom, Node argTo, long argCost){
                (from, to, cost) = (argFrom, argTo, argCost);
            }
            public override string ToString(){
                return $"(to {to.id}, cost {cost})";
            }
        }

        // ダイクストラで探索する用のノードデータ構造
        class NodeForDijkstra : IComparable<NodeForDijkstra>{
            public int id;
            public long cost;
            public int CompareTo(NodeForDijkstra other){
                // コストの小さいほうから処理したいのでそのように並び順を定義する
                return cost.CompareTo(other.cost) * -1;
            }
        }
        // サブクラス ここまで

        public void AddEdge(int idFrom, int idTo, long cost){
            if(idFrom >= nodes.Length || idTo >= nodes.Length){
                throw new IndexOutOfRangeException($"[[ vertのindexは0から{nodes.Length-1}までですが、範囲外のindexが渡されました。idFrom:{idFrom}, idTo:{idTo} ]]");
            }
            Edge newEdge = new Edge(nodes[idFrom], nodes[idTo], cost);
            edges.Add(newEdge);
            nodes[idFrom].AddAdjEdge(newEdge);
        }

        public void AddUndirectedEdge(int id1, int id2, long cost){
            AddEdge(id1, id2, cost);
            AddEdge(id2, id1, cost);
        }

        // ダイクストラ法により最短経路のコストを求める。
        // 注意 : 経路がない場合は ret[i] = long.MaxValue になる。
        // 戻り値 : ret[i] = 始点から頂点iまでの最短経路のコスト
        public long[] Dijkstra(int startVertId){
            // 初期化
            var costs = new long[nodes.Length];
            for(int i=0; i<costs.Length; i++){
                costs[i] = long.MaxValue;
            }
            costs[startVertId] = 0;
            var que = new PriorityQueue<NodeForDijkstra>(nodes.Length);
            que.Push(new NodeForDijkstra{id = startVertId, cost = 0});
            // 探索
            while(que.HasValue){
                var vert = que.Pop();
                var id = vert.id;
                // 記録されているコストと異なるときは無視（Queueにいる間により効率的な経路が見つかったということ）
                if(vert.cost != costs[id]) continue;
                // 注目頂点から繋がっている箇所を見る
                foreach(var edge in nodes[id].adjEdges){
                    var dstId = edge.to.id;
                    long dstCost = costs[id] + edge.cost;
                    if(dstCost < costs[dstId]){
                        costs[dstId] = dstCost;
                        que.Push(new NodeForDijkstra{id = dstId, cost = dstCost});
                    }
                }
            }
            return costs;
        }

        public override string ToString(){
            var sb = new StringBuilder();
            sb.Append("Graph :\n");
            sb.Append("    Verts :\n");
            for(int i=0; i<nodes.Length; i++){
                sb.Append($"        {i} : Edges : {{ ");
                foreach(var edge in nodes[i].adjEdges){
                    sb.Append($"{edge} , ");
                }
                sb.Remove(sb.Length-3,2); // 最後の", "を削除
                sb.Append("}\n");
            }
            return sb.ToString();
        }
    }
    // Graph ここまで
}