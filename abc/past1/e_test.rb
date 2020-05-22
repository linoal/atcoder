require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'e.rb')

in1 = <<~TEXT
6 7
1 1 2
1 2 3
1 3 4
1 1 5
1 5 6
3 1
2 6
TEXT

exp1 = <<~TEXT
NYYNYY
NNYNNN
NNNYNN
NNNNNN
NNNNNY
YNNNYN
TEXT

# in2 = <<~TEXT
# input2
# TEXT

# in3 = <<~TEXT
# input3
# TEXT

t.add(in1, exp1)
# t.add(in2, 'expected2')
# t.add(in3, 'expected3')
t.run