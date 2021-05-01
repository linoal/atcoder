using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ZONe2021.SolverDExtensions;
using static System.Math;
using static System.Console;
using static System.Linq.Enumerable;
using static System.Numerics.BitOperations;

namespace ZONe2021{
    class SolverD{
        static void Main(){
            Debug.isDebugMode = false;
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            new SolverD().Solve();
            Out.Flush();
        }

        public void Solve(){
            checked{
                char[] S = Get.Str().ToCharArray();
                bool r = false;
                Deque<char> q = new Deque<char>(S.Length);
                
                for(int i=0; i<S.Length; i++){
                    if(S[i] == 'R'){
                        r = !r;
                    }else{
                        if(!r){
                            if(q.Count != 0 && q.PeekBack() == S[i]){
                                q.PopBack();
                            }else{
                                q.PushBack(S[i]);
                            }
                        }else{
                            if(q.Count != 0 && q.PeekFront() == S[i]){
                                q.PopFront();
                            }else{
                                q.PushFront(S[i]);
                            }
                        }
                        Debug.Put(q);
                    }
                }

                var ans = new char[q.Count];
                for(int i=0; i<ans.Length; i++){
                    if(!r){
                        ans[i] = q.PopFront();
                    }else{
                        ans[i] = q.PopBack();
                    }
                    
                }
                WriteLine(ans);


            }
        }


        // 双方向キュー
        // indexの少ないほうがFront、多いほうがBack
        // 追加 O(1) , 左端または右端からのPop O(1)
        // ランダムアクセス O(1)
        // 挿入 O(N) , 削除 O(N)
        // capacityを超えた時に再割り当てが発生。
        class Deque<T>{
            T[] buf;
            int offset, capacity;
            public int Count{get; private set;}

            public Deque(int cap){ buf = new T[capacity = cap]; }
            public Deque(){ buf = new T[capacity = 16]; }
            public T this[int index]{
                get{ return buf[GetIndex(index)]; }
                set{ buf[GetIndex(index)] = value; }
            }

            private int GetIndex(int index){
                if( index >= capacity ){
                    throw new IndexOutOfRangeException("Deque: out of range.");
                }
                var ret = index + offset;
                if( ret >= capacity ){
                    ret -= capacity;
                }
                return ret;
            }

            public void PushFront(T item){
                // WriteLine($"pushfront called. buf.Length:{buf.Length}, count:{Count}, capacity:{capacity}");
                if( Count == capacity ) Extend();
                if( --offset < 0) offset += buf.Length;
                // WriteLine($"offset: {offset} , Count: {Count}, buf.Length: {buf.Length}");
                buf[offset] = item;
                Count++;
            }

            public T PopFront(){
                if( Count == 0 ){
                    throw new InvalidOperationException("Deque: PopFront() called while collection is empty.");
                }
                --Count;
                var ret = buf[offset++];
                if(offset >= capacity) offset -= capacity;
                return ret;
            }

            public T PeekFront(){
                if( Count == 0 ){
                    throw new InvalidOperationException("Deque: Peeking from empty queue.");
                }
                return this[0];
            }

            public void PushBack(T item){
                if( Count == capacity ) Extend();
                var id = Count++ + offset;
                if( id >= capacity ) id -= capacity;
                // WriteLine($"pushback called. id:{id}, Count:{Count}, capacity:{capacity}");
                buf[id] = item;
            }

            public T PopBack(){
                if( Count == 0 ){
                    throw new InvalidOperationException("Deque: PopBack() called while collection is empty.");
                }
                return buf[GetIndex(--Count)];
            }

            public T PeekBack(){
                if( Count == 0 ){
                    throw new InvalidOperationException("Deque: Peeking from empty queue.");
                }
                return this[Count-1];
            }

            public void Insert(int index, T item){
                if( index > Count ) throw new IndexOutOfRangeException();
                PushFront(item);
                for( int i = 0; i < index; i++){
                    this[i] = this[i+1];
                }
                this[index] = item;
            }

            public T RemoveAt(int index){
                if( index < 0 || index >= Count ) throw new IndexOutOfRangeException();
                var ret = this[index];
                for( int i = index; i > 0; i-- ){
                    this[i] = this[i-1];
                }
                PopFront();
                return ret;
            }

            private void Extend(){
                T[] newBuffer = new T[capacity << 1];
                if( offset > capacity - Count ){
                    var len = buf.Length - offset;
                    Array.Copy(buf, offset, newBuffer, 0, len);
                    Array.Copy(buf, 0, newBuffer, len, Count - len);
                }else{
                    Array.Copy(buf, offset, newBuffer, 0, Count);
                }
                buf = newBuffer;
                offset = 0;
                capacity <<= 1;
            }

                public override string ToString()
                {
                    var sb = new StringBuilder();
                    sb.Append("Deque: { ");
                    for(int i=0; i<Count; i++){
                        sb.Append(this[i].ToString());
                        if(i < Count - 1){
                            sb.Append(" ,");
                        }
                    }
                    sb.Append(" }");
                    return sb.ToString();
                }
        
        } // class Deque ここまで


      


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
    namespace SolverDExtensions{
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
