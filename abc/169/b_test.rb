require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')

in1 = <<~TEXT
2
1000000000 1000000000
TEXT

exp1 = <<~TEXT
1000000000000000000
TEXT

in2 = <<~TEXT
3
101 9901 999999000001
TEXT

exp2 = <<~TEXT
-1
TEXT

in3 = <<~TEXT
31
4 1 5 9 2 6 5 3 5 8 9 7 9 3 2 3 8 4 6 2 6 4 3 3 8 3 2 7 9 5 0
TEXT

exp3 = <<~TEXT
0
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.run