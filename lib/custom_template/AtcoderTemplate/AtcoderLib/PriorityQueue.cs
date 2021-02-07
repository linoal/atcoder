using System;
using System.Collections.Generic;
using System.Text;

namespace AtcoderLib{
    // 大きいほうから取る優先度付きキュー。
        // 要素の型TはIComparable<T>の実装が必要(CompareToを実装)。
        // Push(),Pop()の計算量はO(logN)。
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
}