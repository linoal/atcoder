require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'e.rb')

in1 = <<~TEXT
3 2 3
1 2
2 3
5 10 15
1 2
2 1 20
1 1
TEXT

exp1 = <<~TEXT
10
10
20
TEXT

in2 = <<~TEXT
30 10 20
11 13
30 14
6 4
7 23
30 8
17 4
6 23
24 18
26 25
9 3
18 4 36 46 28 16 34 19 37 23 25 7 24 16 17 41 24 38 6 29 10 33 38 25 47 8 13 8 42 40
2 1 9
1 8
1 9
2 20 24
2 26 18
1 20
1 26
2 24 31
1 4
2 21 27
1 25
1 29
2 10 14
2 2 19
2 15 36
2 28 6
2 13 5
1 12
1 11
2 14 43
TEXT

exp2 = <<~TEXT
18
19
37
29
8
24
18
25
46
10
18
42
23
4
17
8
24
7
25
16
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