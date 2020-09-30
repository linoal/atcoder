using System;

namespace AtcoderLib{

    // 双方向キュー
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
    } // class Deque ここまで
}