require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')

in1 = <<~TEXT
2 1 6
2 1 1
1 1
1 2
2 2 1
1 1
1 2
TEXT

exp1 = <<~TEXT
1
0
0
0
TEXT

in2 = <<~TEXT
5 5 30
1 3
2 3 5
1 3
2 2 1
2 4 5
2 5 2
2 2 3
1 4
2 4 1
2 2 2
1 1
1 5
2 5 3
2 4 4
1 4
1 2
2 3 3
2 4 3
1 3
1 5
1 3
2 1 3
1 1
2 2 4
1 1
1 4
1 5
1 4
1 1
1 5
TEXT

exp2 = <<~TEXT
0
4
3
0
3
10
9
4
4
4
0
0
9
3
9
0
3
TEXT

# in3 = <<~TEXT
# input3
# TEXT

# exp3 = <<~TEXT
# expect3
# TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
# t.add(in3, exp3.chomp)
t.run