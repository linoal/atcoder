using System;

namespace AtcoderLib{

    static class Probability{

        // nCk mod m を求める。 O(n^2)。
        // コンストラクタでdpテーブルを計算し、Getでテーブルから結果を取得する。
        // パスカルの三角形を利用。
        public class Combination{
            long[][] pascal;

            // nCk に関するdpテーブル(要素数 n*n/2)を作成する。O(n^2)。
            public Combination(long n, long mod = long.MaxValue){
                pascal = new long[n+1][];
                for(int i=0; i<n+1; i++){
                    pascal[i] = new long[i+1];
                    pascal[i][0] = 1;
                    pascal[i][i] = 1;
                }
                for(int i=2; i<n+1; i++){
                    for(int j=1; j<i; j++){
                        pascal[i][j] = ( pascal[i-1][j-1] + pascal[i-1][j] ) % mod;
                    }
                }
            }

            // nCk をdpテーブルから取得する。O(1)。
            public long Get(long n, long k){
                return pascal[n][k];
            }

            // pascal dpテーブルを表示
            public void DebugPrint(){
                for(int i=0; i<pascal.Length; i++){
                    for(int j=0; j<pascal[i].Length; j++){
                        Console.Write($"{pascal[i][j]} ");
                    }
                    Console.Write("\n");
                }
            }
        }
        // Combinationクラス ここまで
    }
}