using System;
using static System.Console;

namespace PROJECT_NAME
{
    class ContestMain
    {
        static void Main(){
chooseQ:
            WriteLine("Choose operation (ea):");
            var q = ReadLine();
            switch(q)
            {
                case "ea":
                    SolverA.Solve();
                    break;
                case "ta":
                    TestA.Test();
                    break;
                default:
                    goto chooseQ;
            }
        }
    }
}