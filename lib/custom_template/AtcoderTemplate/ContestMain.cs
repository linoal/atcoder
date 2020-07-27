using System;
using static System.Console;

namespace PROJECT_NAME
{
    static class ContestMain
    {
        static void Main(){
chooseOp:
            WriteLine("Choose operation [e,t][a,b,c,d,e]:"); // (Exec / Test) question (A/B/C/D/E)
            var q = ReadLine();
            switch(q)
            {
                case "ea":
                    new SolverA().Solve();
                    break;
                case "ta":
                    Tester.TestQuestion(new SolverA().Solve, "TestCasesA.txt");
                    break;
                case "eb":
                    new SolverB().Solve();
                    break;
                case "tb":
                    Tester.TestQuestion(new SolverB().Solve, "TestCasesB.txt");
                    break;
                case "ec":
                    new SolverC().Solve();
                    break;
                case "tc":
                    Tester.TestQuestion(new SolverC().Solve, "TestCasesC.txt");
                    break;
                case "ed":
                    new SolverD().Solve();
                    break;
                case "td":
                    Tester.TestQuestion(new SolverD().Solve, "TestCasesD.txt");
                    break;
                case "ee":
                    new SolverE().Solve();
                    break;
                case "te":
                    Tester.TestQuestion(new SolverE().Solve, "TestCasesE.txt");
                    break;

                default:
                    goto chooseOp;
            }
        }
    }
}