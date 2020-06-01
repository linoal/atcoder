require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'c.rb')

in1 = <<~TEXT
198 1.10

TEXT

exp1 = <<~TEXT
217
TEXT

in2 = <<~TEXT
1 0.01
TEXT

exp2 = <<~TEXT
0
TEXT

in3 = <<~TEXT
1000000000000000 9.99
TEXT

exp3 = <<~TEXT
9990000000000000
TEXT

in4 = <<~TEXT
1000000000000000 0.01
TEXT

exp4 = <<~TEXT
10000000000000
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.add(in4, exp4.chomp)
t.run