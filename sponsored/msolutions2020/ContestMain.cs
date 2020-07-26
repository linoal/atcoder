using System;
using static System.Console;

namespace msolutions2020
{
    class ContestMain
    {
        static void Main(){
chooseOp:
            WriteLine("Choose operation [e,t][a,b,c,d,e]:"); // (Exec / Test) question (A/B/C/D/E)
            var q = ReadLine();
            switch(q)
            {
                case "ea":
                    SolverA.Solve();
                    break;
                case "ta":
                    Tester.TestQuestion(SolverA.Solve, "TestCasesA.txt");
                    break;
                case "eb":
                    SolverB.Solve();
                    break;
                case "tb":
                    Tester.TestQuestion(SolverB.Solve, "TestCasesB.txt");
                    break;
                case "ec":
                    SolverC.Solve();
                    break;
                case "tc":
                    Tester.TestQuestion(SolverC.Solve, "TestCasesC.txt");
                    break;
                case "ed":
                    SolverD.Solve();
                    break;
                case "td":
                    Tester.TestQuestion(SolverD.Solve, "TestCasesD.txt");
                    break;
                case "ee":
                    SolverE.Solve();
                    break;
                case "te":
                    Tester.TestQuestion(SolverE.Solve, "TestCasesE.txt");
                    break;

                default:
                    goto chooseOp;
            }
        }
    }
}