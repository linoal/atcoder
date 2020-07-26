using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

namespace msolutions2020
{
    class SolverD
    {
        static void Main()
        {
            Solve();
        }

        public static void Solve()
        {
            var N = int.Parse(ReadLine());
            int[] A = ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            long money = 1000;
            long stock = 0;
            for(int day=0; day<N; day++)
            {
                if(day == N-1)
                {
                    money += stock * A[day];
                    break;
                }
                if(A[day] < A[day+1])
                {
                    long buyN = money / A[day];
                    stock += buyN;
                    money -= buyN * A[day];
                }else if(A[day] > A[day+1])
                {
                    money += stock * A[day];
                    stock = 0;
                }
                // WriteLine($"money={money}, stock={stock}");
            }
            WriteLine(money);
        }
    }
}
