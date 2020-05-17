require "~/atcoder/lib/test"
t = Test.new('./d2.rb')

in1 = <<~TEXT
4 4
1 2
2 3
3 4
4 2
TEXT

in2 = <<~TEXT
6 9
3 4
6 1
2 4
5 3
4 6
1 5
6 2
4 5
5 6
TEXT

# in3 = <<~TEXT
# input3
# TEXT

exp1 = <<~TEXT
Yes
1
2
2
TEXT

exp2 = <<~TEXT
Yes
6
5
5
1
1
TEXT

t.add(in1, exp1)
t.add(in2, exp2)
# t.add(in3, 'expected3')
t.run