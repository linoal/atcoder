using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

namespace PROJECT_NAME
{
    class SolverB
    {
        static void Main()
        {
            Solve();
        }

        public static void Solve()
        {
            var a = int.Parse(ReadLine());
            var b = int.Parse(ReadLine());
            WriteLine(a+b);
            WriteLine(a-b);
        }
    }
}
