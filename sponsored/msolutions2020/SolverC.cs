using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using static System.Math;
using static System.Console;

namespace msolutions2020
{
    class SolverC
    {
        static void Main()
        {
            SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false});
            Solve();
            Out.Flush();
        }

        public static void Solve()
        {
            var _nk = ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            (int N, int K) = (_nk[0], _nk[1]);
            int[] A = ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            for (int i=K; i<N; i++)
            {
                WriteLine(A[i-K] < A[i] ? "Yes" : "No");
            }
        }
    }
}
