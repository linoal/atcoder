using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

namespace msolutions2020
{
    class SolverA
    {
        static void Main()
        {
            Solve();
        }

        public static void Solve()
        {
            var grade = 0;
            var x = int.Parse(ReadLine());
            if(x <= 599) grade = 8;
            else if(x <= 799) grade = 7;
            else if(x <= 999) grade = 6;
            else if(x <= 1199) grade = 5;
            else if(x <= 1399) grade = 4;
            else if(x <= 1599) grade = 3;
            else if(x <= 1799) grade = 2;
            else grade = 1;
            WriteLine(grade);
        }
    }
}
