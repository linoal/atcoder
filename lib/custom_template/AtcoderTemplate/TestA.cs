using System;

namespace PROJECT_NAME
{
    class TestA
    {
        static public void Test(){
            new TestCase("TestCasesA.txt", 1).Test(SolverA.Solve);
        }
        
    }
}