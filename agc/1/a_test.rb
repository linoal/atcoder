require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'a.rb')

# in0 = <<~TEXT
# 1
# 29384293847243 454353412 332423423 934923490 1
# TEXT

# exp0 = <<~TEXT
# 3821859835
# TEXT

in2 = <<~TEXT
1
32 10 8 5 4
TEXT

in1 = <<~TEXT
5
11 1 2 4 8
11 1 2 2 8
32 10 8 5 4
29384293847243 454353412 332423423 934923490 1
900000000000000000 332423423 454353412 934923490 987654321
TEXT

exp1 = <<~TEXT
20
19
26
3821859835
23441258666
TEXT

# in2 = <<~TEXT
# input2
# TEXT

# exp2 = <<~TEXT
# expect2
# TEXT

# in3 = <<~TEXT
# input3
# TEXT

# exp3 = <<~TEXT
# expect3
# TEXT

t.add(in1, exp1.chomp)
# t.add(in2, exp2.chomp)
# t.add(in3, exp3.chomp)
t.run