using System;
namespace AtcoderLib{
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
                        maxStrLen = Math.Max(maxStrLen, this[i,j].ToString().Length);
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
}