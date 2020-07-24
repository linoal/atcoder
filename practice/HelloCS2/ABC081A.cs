using System;
using System.Linq;
using static System.Console;
class ABC081A{
    static void Main(){
        var cnt = ReadLine().ToCharArray().Count(c => c=='1');
        WriteLine(cnt.ToString());
    }
}