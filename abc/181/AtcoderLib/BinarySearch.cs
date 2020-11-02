using System;

namespace AtcoderLib{
    
    class BinarySearch{

        // 昇順の配列に対し、指定された値以上の値を持つ最初のインデックスを求める。
        // すべて指定された値未満である場合は、最後のインデックス+1 (=array.Length)が返る。
        public static int LowerBound<T>(T[] array, T val) where T: struct, IComparable<T>{
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