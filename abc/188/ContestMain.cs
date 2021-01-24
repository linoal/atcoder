using System;
using static System.Console;

namespace ABC188
{
    
    static class ContestMain
    {
        static readonly string TEXT_FOLDER = "Solver";
        static void Main(){
chooseOp:
            WriteLine("Choose operation [e,t][a,b,c,d,e,f]:"); // (Exec / Test) question (A/B/C/D/E)
            var q = ReadLine();
            switch(q)
            {
                case "ea":
                    new SolverA().Solve();
                    break;
                case "ta":
                    Tester.TestQuestion(new SolverA().Solve, $"{TEXT_FOLDER}/TestCasesA.txt");
                    break;
                case "eb":
                    new SolverB().Solve();
                    break;
                case "tb":
                    Tester.TestQuestion(new SolverB().Solve, $"{TEXT_FOLDER}/TestCasesB.txt");
                    break;
                case "ec":
                    new SolverC().Solve();
                    break;
                case "tc":
                    Tester.TestQuestion(new SolverC().Solve, $"{TEXT_FOLDER}/TestCasesC.txt");
                    break;
                case "ed":
                    new SolverD().Solve();
                    break;
                case "td":
                    Tester.TestQuestion(new SolverD().Solve, $"{TEXT_FOLDER}/TestCasesD.txt");
                    break;
                case "ee":
                    new SolverE().Solve();
                    break;
                case "te":
                    Tester.TestQuestion(new SolverE().Solve, $"{TEXT_FOLDER}/TestCasesE.txt");
                    break;
                case "ef":
                    new SolverF().Solve();
                    break;
                case "tf":
                    Tester.TestQuestion(new SolverF().Solve, $"{TEXT_FOLDER}/TestCasesF.txt");
                    break;

                default:
                    goto chooseOp;
            }
        }
    }
}