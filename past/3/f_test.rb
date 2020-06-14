require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'f.rb')

in1 = <<~TEXT
2
yc
ys
TEXT

exp1 = <<~TEXT
yy
TEXT

in2 = <<~TEXT
2
rv
jh
TEXT

exp2 = <<~TEXT
-1
TEXT

in3 = <<~TEXT
1
a
TEXT

exp3 = <<~TEXT
a
TEXT

in4 = <<~TEXT
3
abc
def
xyc
TEXT


exp4 = <<~TEXT
cdc
TEXT

in5 = <<~TEXT
5
abcde
fghij
vwxyz
jklmn
efghi
TEXT

exp5 = <<~TEXT
ejvje
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.add(in4, exp4.chomp)
t.add(in5, exp5.chomp)
t.run