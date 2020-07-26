using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Linq;
using static System.Console;

namespace PROJECT_NAME
{
    class Tester
    {

        public static void TestQuestion(Action targetAct, string fileName)
        {
            var caseNum = 1;
            var testCnt = 0;
            var testFailCnt = 0;
            while(true)
            {
                bool? result = TestCase(targetAct, fileName, caseNum++);
                if( result == null ) break;
                testCnt++;
                if( !result.Value ) testFailCnt++;
            }
            WriteLine("\u001b[36m------------------------------------------\u001b[0m");
            WriteLine(testFailCnt==0 ? "*\u001b[36m\u001b[1m ALL TEST PASSED \u001b[0m" : $"*\u001b[33m {testFailCnt} TEST FAILED \u001b[0m");
            WriteLine("\u001b[36m------------------------------------------\u001b[0m");
        }

        // 戻り値 : caseNum番のテストケースが存在しない場合はnull、存在する場合はテストをしてその成否がboolで返る。
        public static bool? TestCase(Action targetAct, string fileName, int caseNum)
        {
            var caseInput = ReadDSLChunk(fileName, $"[Case {caseNum} Input]");
            var caseExpect = ReadDSLChunk(fileName, $"[Case {caseNum} Expect]");
            if (caseInput == "" && caseExpect == "") return null;
            var savedConsoleOut = Console.Out;
            using (var input = new StringReader(caseInput))
            using (var output = new StringWriter())
            {
                SetOut(output);
                SetIn(input);
                var stopwatch = Stopwatch.StartNew();
                targetAct();
                stopwatch.Stop();
                var actual = output.ToString().Trim();
                var isCorrect = actual == caseExpect;
                
                SetOut(savedConsoleOut); // ここから上の Console.Out はテスト対象への入力になるので標準出力には表示されない
                // 標準出力にテスト結果を出力
                WriteLine($"\u001b[36m[{caseNum}]------------------------------------------");
                if (caseExpect.Count(c => c == '\n') > 0) // 複数行か否かで表示形式を切り替えたほうが綺麗
                {
                    WriteLine($"expected:\u001b[0m"); Write(caseExpect);
                }else
                {
                    Write($"expected:\u001b[0m {caseExpect}");
                }
                Write("\u001b[36m"); // 色
                if (actual.Count(c => c == '\n') > 0)
                {
                    WriteLine($"\nactual:\u001b[0m"); WriteLine(actual);
                }else
                {
                    WriteLine($"\tactual:\u001b[0m {actual}");
                }
                // WriteLine($"expected: \"{caseExpect}\", actual: \"{actual}\"");
                Write(isCorrect ? "\u001b[36m\u001b[1mPASSED\u001b[0m" : "\u001b[33mFAILED\u001b[0m");
                WriteLine($"\t\u001b[36mtime: {stopwatch.ElapsedMilliseconds}ms \u001b[0m");
                return isCorrect;
            }
        }

        // テキストファイルの一部を読む。読む範囲は指定チャンクタグ(例: "[Case 1 Input]")の次行から次のチャンクタグまたはEOFまで
        static string ReadDSLChunk(string fileName, string chunkTag)
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
                                reader.ReadToEnd(); // 次のタグが来たのでファイル読み込み終了。中身は見ずにポインタだけ最後まで進める。
                                break;
                            }
                            sb.AppendLine(line);
                            break;
                    }
                }
            }
            return sb.ToString().Trim();
        }
    }
}