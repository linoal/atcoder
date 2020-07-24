using System;
using System.IO;
using System.Text;
using static System.Console;

namespace PROJECT_NAME
{
    class TestCase
    {
        public int CaseNum{get;}
        public string Input{get;}
        public string Expect{get;}

        public TestCase(string fileName, int caseNum)
        {
            CaseNum = caseNum;
            Input = ReadDSLChunk(fileName, $"[Case {caseNum} Input]");
            Expect = ReadDSLChunk(fileName, $"[Case {caseNum} Expect]");
        }

        public bool Test(Action targetAct)
        {
            var savedConsoleOut = Console.Out;
            using (var input = new StringReader(Input))
            using (var output = new StringWriter())
            {
                SetOut(output);
                SetIn(input);
                targetAct();
                var actual = output.ToString().Trim();
                var isCorrect = actual == Expect;
                
                SetOut(savedConsoleOut); // ここから上の Console.Out はテスト対象への入力になるので標準出力には表示されない
                WriteLine($"[{CaseNum}]------------------------------------------");
                WriteLine($"expected: \"{Expect}\", actual: \"{actual}\"");
                WriteLine(isCorrect ? "Test Passed" : "Test Failed");
                return isCorrect;
            }
        }

        string ReadDSLChunk(string fileName, string chunkTag)
        {
            var sb = new StringBuilder();
            using (var reader = new StreamReader(fileName))
            {
                var state = 0;
                while( ! reader.EndOfStream )
                {
                    var line = reader.ReadLine();
                    switch(state)
                    {
                        case 0: // [Case n Input] を検索中
                            if (line == chunkTag)
                            {
                                state = 1;
                            }
                            break;
                        case 1: // [Case n Input]の中身
                            if (line.StartsWith("[Case"))
                            {
                                reader.ReadToEnd();
                                break;
                            }
                            sb.Append(line);
                            break;
                    }
                }
            }
            return sb.ToString();
        }
    }
}