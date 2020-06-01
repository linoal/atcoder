require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'a.rb')

in1 = <<~TEXT
2 5
TEXT

exp1 = <<~TEXT
10
TEXT

in2 = <<~TEXT
100 100
TEXT

exp2 = <<~TEXT
10000
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