using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;

namespace AtcoderLib{

    // char[,]で構成された迷路。
    // DebugPut()でデバッグ出力する。
    // BFSSearchで幅優先探索をする。
    class Maze{
        /**
        BFSの使い方例：
            var maze = new Maze(charMap);
            maze.isDebugMode = false;
            var cost = maze.BFSSearch(SY,SX);
            WriteLine(cost);
        **/
        public char[,] map;
        public bool isDebugMode = false;
        long[,] costs;
        Queue<Node> que = new Queue<Node>();
        int H; int W;
        public Maze(char[,] _map){
            map = _map;
            H = map.GetLength(0);
            W = map.GetLength(1);
            costs = new long[H,W];
            for(int i=0; i<H; i++){
                for(int j=0; j<W; j++){
                    costs[i,j] = long.MaxValue;
                }
            }
        }

        public long BFSSearch(int sy, int sx){ // sx,sy はスタート地点座標
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
                        if(Abs(i)+Abs(j) != 1) continue; // 探索対象のマンハッタン距離
                        int ty = y+i; int tx = x+j; // target x,y
                        if(ty<0 || ty >= H || tx<0 || tx >=W) continue;
                        var tc = map[y+i, x+j]; // target char
                        if(tc != '.' && tc!='G') continue; // 探索対象のタイル
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
    // class BFS ここまで
}