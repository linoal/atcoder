    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using static System.Math;
    using static System.Console;
    using static System.Linq.Enumerable;
    using static System.Numerics.BitOperations;
     
    namespace abc176
    {
        class SolverD
        {
            static void Main()
            {
                SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
                new SolverD().Solve();
                Out.Flush();
            }
     
            public void Solve()
            {
                checked
                {
                    (int H, int W) = Get.Tuple<int,int>();
                    (int ch, int cw) = Get.Tuple<int,int>();
                    (int dh, int dw) = Get.Tuple<int,int>();
                    ch--; cw--; dh--; dw--;
                    int[,] maze = new int[H,W];
                    for(int i=0; i<H; i++){
                        char[] l = Get.Str().ToCharArray();
                        for(int j=0; j<W; j++){
                            maze[i,j] = l[j] == '.' ? -1 : -2;
                        }
                    }
                    maze[ch,cw] = 0;
     
                    Deque<Coord> queue = new Deque<Coord>(1100000);
                    queue.PushFront(new Coord{h=ch, w=cw});
                    bool goalReached = false;
                    while(queue.Count > 0){
     
                        // Debug(maze,queue);
     
                        Coord c = queue.PopFront();
                        int ci = maze[c.h,c.w];
     
                        if(c.h == dh && c.w == dw){
                            goalReached = true;
                            // break;
                        }
     
     
                        if(c.w > 0 && IsNewPlace(maze,c.h,c.w-1,ci)){
                            maze[c.h,c.w-1] = ci;
                            queue.PushFront(new Coord{h=c.h, w=c.w-1});
                        }
                        if(c.w < W-1 && IsNewPlace(maze, c.h, c.w+1, ci)){
                            maze[c.h, c.w+1] = ci;
                            queue.PushFront(new Coord{h=c.h, w=c.w+1});
                        }
                        if(c.h > 0 && IsNewPlace(maze, c.h-1, c.w, ci)){
                            maze[c.h-1, c.w] = ci;
                            queue.PushFront(new Coord{h=c.h-1, w=c.w});
                        }
                        if(c.h < H-1 && IsNewPlace(maze, c.h+1, c.w, ci)){
                            maze[c.h+1, c.w] = ci;
                            queue.PushFront(new Coord{h=c.h+1, w=c.w});
                        }
     
                        if(ShouldCheckWarp(maze,c.h,c.w,H,W)){
                            for(int Hdiff=-2; Hdiff<=2; Hdiff++){
                                for(int Wdiff=-2; Wdiff<=2; Wdiff++){
                                    int th = c.h + Hdiff;
                                    int tw = c.w + Wdiff;
                                    if( ! IsInRange(th, tw, H, W)){
                                        continue;
                                    }
                                    if( IsNewPlace(maze, th, tw, ci) ){
                                        maze[th,tw] = ci+1;
                                        queue.PushBack(new Coord{h=th, w=tw});
                                    }
                                }
                            }
                        }
                        
                    }
                    // while ここまで
                    if(goalReached){
                        WriteLine(maze[dh,dw]);
                    }else{
                        WriteLine(-1);
                    }
                }
            }
     
            bool ShouldCheckWarp(int[,] maze, int h, int w, int HL, int WL){
     
                return (h>0 && maze[h-1,w]==-2 )||
                    (h<HL-1 && maze[h+1,w]==-2) ||
                    (w>0 && maze[h,w-1]==-2) ||
                    (w<WL-1 && maze[h,w+1]==-2 );
            }
     
            bool IsNewPlace(int[,] maze, int h, int w, int currentI){
                int ti = maze[h,w];
                if(ti == -2){
                    return false;
                }else if(ti == -1){
                    return true;
                }else if(ti > currentI){
                    return true;
                }
                return false;
            }
     
            bool IsInRange(int th, int tw, int H, int W){
                return th>=0 && tw>=0 && th<H && tw<W;
            }
     
     
     
            struct Coord{
                public int h;
                public int w;
            }
     
            static void Debug(int[,] maze, Deque<Coord> q){
                Write("queue: ");
                for(int i=0; i < q.Count; i++){
                    Write($"({q[i].h},{q[i].w}) ");
                }
                WriteLine();
                for(int i=0; i<maze.GetLength(0); i++){
                    for(int j=0; j<maze.GetLength(1); j++){
                        Write(maze[i,j].ToString().PadLeft(2,' ') + ",");
                    }
                    Write("\n");
                }
                Write("\n");
                ReadLine();
            }

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
            }
     
     
     
            private static class Get
            {
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
            }
        }
    }