require 'date'

# AtCoderでテストする用。
# 利用する時はスニペット atct を利用できる。

# 指定パスのrubyファイルを実行し、その標準入力にinputを渡す。
# そして標準出力で帰ってきた値がexpectedならテストクリア。
# ただし'd:'で始まる出力は回答ではなくデバッグ用出力とみなす。
class Test

    def initialize(ruby_path)
        @ruby_path = ruby_path
        @test_cases = []
        @debug_output_count = 0
    end

    def add(input, expected)
        @test_cases << {input: input, expected: expected}
    end

    def run
        succeed_count = 0
        failed_count = 0
        @test_cases.each.with_index(1) do |c, i|
            r = test_one_case(@ruby_path, c[:input],c[:expected], i )
            succeed_count += 1 if r
            failed_count += 1 unless r
        end
        puts "----------------------------"
        if failed_count == 0 && succeed_count > 0
            puts "* ALL TEST PASSED (#{succeed_count} tests succeed)"
        else
            puts "* TEST FAILED (#{succeed_count} succeed, #{failed_count} failed)"
        end
        puts "* WARNING: REMOVE DEBUG TEXT (debug outputted #{@debug_output_count} times)" if @debug_output_count>0
        puts "----------------------------"
    end


    private

    def test_one_case(ruby_path, input, expected, test_num)
        test_passed = false
        io = IO.popen("ruby #{ruby_path}", 'w+', :err => [:child, :out]) do |io|
            if input.is_a? String
                io.puts input
            else
                puts "Error: 2nd argument in Test::test is wrong. (expected string, actual #{input.class})"
                return false
            end
            time_before = Time.now
            puts "[#{test_num}]-------------------------"
            actual = receive_result(io)
            test_passed = (actual == expected)

            # 結果出力。行数によってフォーマットを分ける。
            if actual.count("\n")==0 && expected.count("\n")==0
                puts "actual:   #{actual}\tjudge: #{test_passed ? 'OK': 'NG'}"
                puts "expected: #{expected}\ttime:  #{ ((Time.now - time_before)*1000).to_i }ms"
            else
                puts "actual:\n#{actual}\nexpected:\n#{expected}\njudge: #{test_passed ? 'OK':'NG'}  time: #{ ((Time.now - time_before)*1000).to_i }ms"
            end
        end
        test_passed
    end

    def receive_result(io)
        loop do
            received = io.read&.chomp
            puts "Error: Result is nil. (expected string)" if received.nil?
            if received[0..1] == 'd:'
                puts "debug: #{received[2..-1]}"
                @debug_output_count += 1
                next
            else
                return received
            end
        end
    end

end