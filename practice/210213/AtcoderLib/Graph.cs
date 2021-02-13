using System;
using System.Collections.Generic;
using System.Text;

namespace AtcoderLib{
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

            // サブクラス ここから
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
            // サブクラス ここまで

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
        // Graph ここまで
}