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

in2 = <<~TEXT
2 1
1 1 2
TEXT

exp2 = <<~TEXT
NY
NN
TEXT

in3 = <<~TEXT
2 2
1 1 2
2 2
TEXT

exp3 = <<~TEXT
NY
YN
TEXT

in4 = <<~TEXT
3 5
1 1 2
1 1 3
1 2 1
1 3 1
3 3
TEXT

# NYY
# YNN
# YNN

exp4 = <<~TEXT
NYY
YNN
YYN
TEXT

in5 = <<~TEXT
3 6
1 1 2
1 1 3
1 2 1
1 3 1
3 3
2 1
TEXT

exp5 = <<~TEXT
NYY
YNN
YYN
TEXT

# in5 = <<~TEXT
# TEXT

# exp5 = <<~TEXT

# TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.add(in4, exp4.chomp)
t.add(in5, exp5.chomp)
t.run

# NYN
# YNY
# YNN