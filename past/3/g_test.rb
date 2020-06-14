require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'g.rb')

in1 = <<~TEXT
1 2 2
1 1
TEXT

exp1 = <<~TEXT
3
TEXT

in2 = <<~TEXT
1 2 2
2 1
TEXT

exp2 = <<~TEXT
2
TEXT

in3 = <<~TEXT
5 -2 3
1 1
-1 1
0 1
-2 1
-3 1
TEXT

exp3 = <<~TEXT
6
TEXT

in4 = <<~TEXT
8 2 2
1 1
1 2
1 3
2 1
2 3
3 1
3 2
3 3
TEXT

exp4 = <<~TEXT
-1
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.add(in4, exp4.chomp)
t.run