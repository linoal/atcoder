using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;

namespace HelloCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var ab = ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            var a = ab[0]; var b = ab[1];
            WriteLine((a % 2 == 0 || b % 2 == 0) ? "Even" : "Odd");
        }
    }
}
