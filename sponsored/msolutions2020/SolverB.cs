using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

namespace msolutions2020
{
    class SolverB
    {
        static void Main()
        {
            Solve();
        }

        public static void Solve()
        {
            int[] cards = ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
            var k = int.Parse(ReadLine());
            for(int i=0; i<k; i++)
            {
                if (cards[1] <= cards[0])
                {
                    cards[1] *= 2;
                    continue;
                }else if( cards[2] <= cards[1])
                {
                    cards[2] *= 2;
                    continue;
                }else
                {
                    break;
                }
                
            }
            WriteLine(cards[0] < cards[1] && cards[1] < cards[2] ? "Yes" : "No");
        }
    }
}
