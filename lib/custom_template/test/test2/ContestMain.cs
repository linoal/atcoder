using System;
using static System.Console;

namespace test2
{
    class ContestMain
    {
        static void Main(){
chooseQ:
            WriteLine("Choose question (a):");
            var q = ReadLine();
            switch(q)
            {
                case "a":
                    SolverA.Solve();
                    break;
                default:
                    goto chooseQ;
            }
        }
    }
}