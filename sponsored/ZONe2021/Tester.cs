using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Console;

namespace ZONe2021
{
    static class Tester
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
        // ただし、色付き(ANSIコード)を含む行はデバッグ用とみなして正誤判定から除外する。
        public static bool? TestCase(Action targetAct, string fileName, int caseNum)
        {
            var caseInput = ReadTestCaseInput(caseNum, fileName);
            var caseExpect = ReadTestCaseExpect(caseNum, fileName);
            if (caseInput == "" && caseExpect == "") return null;
            var savedConsoleOut = Console.Out;
            using (var input = new StringReader(caseInput))
            using (var output = new StringWriter())
            {
                SetOut(output);
                SetIn(input);
                var stopwatch = Stopwatch.StartNew();
                try{
                    targetAct(); // ここでテスト対象のメソッドを実行
                }catch(Exception e){
                    WriteLine(e.ToString());
                }
                stopwatch.Stop();
                var actual_withDebug = output.ToString().Trim();
                var actual_withoutDebug = RemoveColoredLine(actual_withDebug).Trim();
                var isCorrect = actual_withoutDebug == caseExpect;
                
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
                if (actual_withDebug.Count(c => c == '\n') > 0)
                {
                    WriteLine($"\nactual:\u001b[0m"); WriteLine(actual_withDebug);
                }else
                {
                    WriteLine($"\tactual:\u001b[0m {actual_withDebug}");
                }
                // WriteLine($"expected: \"{caseExpect}\", actual: \"{actual}\"");
                Write(isCorrect ? "\u001b[36m\u001b[1mPASSED\u001b[0m" : "\u001b[33mFAILED\u001b[0m");
                WriteLine($"\t\u001b[36mtime: {stopwatch.ElapsedMilliseconds}ms \u001b[0m");
                return isCorrect;
            }
        }

        static string RemoveColoredLine(string str){
            string[] strs = str.Split("\n");
            for(int i=0; i<strs.Length; i++){
                if(strs[i].Contains("\u001b[")){
                    strs[i] = "";
                }
            }
            return String.Join("\n",strs);
        }

        static string ReadTestCaseInput(int caseNum, string fileName)
        {
            return ReadDSLChunk(fileName, "[Case Input]", caseNum);
        }

        static string ReadTestCaseExpect(int caseNum, string fileName)
        {
            return ReadDSLChunk(fileName, "[Case Expect]", caseNum);
        }

        // テキストファイルの一部を読む。読む範囲は指定のチャンクタグ(例: "[Case Input]")が tagNumber 番目に現れた箇所の次行から次のチャンクタグまたはEOFまで
        static string ReadDSLChunk(string fileName, string chunkTag, int tagNumber)
        {
            var sb = new StringBuilder();
            using (var reader = new StreamReader(fileName))
            {
                var state = 0;
                int tagCnt = 0;
                string prevLine = "";
                while( ! reader.EndOfStream )
                {
                    var line = reader.ReadLine();
                    if (line.StartsWith("//")) continue; // 「//」はコメント行なので無視
                    if (line.StartsWith("[End]")) break; // [END]以降は無視
                    
                    switch(state)
                    {
                        case 0: // 指定チャンクタグを検索中
                            if (line == chunkTag)
                            {
                                tagCnt++;
                                if (tagCnt == tagNumber)
                                {
                                    state = 1;
                                }
                                
                            }
                            break;
                        case 1: // 指定チャンクタグの中身を読込中
                            if (line.StartsWith("[Case"))
                            {
                                reader.ReadToEnd(); // 次のタグが来たのでファイル読み込み終了。中身は見ずにポインタだけ最後まで進める。
                                break;
                            }else if (line.StartsWith("[Rep")) // [Rep n]
                            {
                                // WriteLine(line);
                                // WriteLine($"reg: {Regex.Match(line, @"\[Rep\s*(\d+)").Groups[1].Value}");
                                int repNum = int.Parse(Regex.Match(line, @"\[Rep\s*(\d+)").Groups[1].Value);
                                for (int i = 0; i < repNum - 1; i++)
                                {
                                    sb.AppendLine(prevLine);
                                }
                            }else
                            {
                                sb.AppendLine(line);
                            }
                            break;
                    }
                    prevLine = line;
                }
            }
            return sb.ToString().Trim();
        }

        static string Magenta(string s){
            return $"\u001b[35m{s}\u001b[0m";
        }

        
    }
}