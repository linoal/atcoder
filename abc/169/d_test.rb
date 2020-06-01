require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd.rb')

in1 = <<~TEXT
24
TEXT

exp1 = <<~TEXT
3
TEXT

in2 = <<~TEXT
1
TEXT

exp2 = <<~TEXT
0
TEXT

in3 = <<~TEXT
64
TEXT

exp3 = <<~TEXT
3
TEXT

in4 = <<~TEXT
1000000007
TEXT

exp4 = <<~TEXT
1
TEXT

in5 = <<~TEXT
997764507000
TEXT

exp5 = <<~TEXT
7
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.add(in4, exp4.chomp)
t.add(in5, exp5.chomp)
t.run