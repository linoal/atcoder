require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'a.rb')

in1 = <<~TEXT
AbC
ABc
TEXT

exp1 = <<~TEXT
case-insensitive
TEXT

in2 = <<~TEXT
xyz
xyz
TEXT

exp2 = <<~TEXT
same
TEXT

in3 = <<~TEXT
aDs
kjH
TEXT

exp3 = <<~TEXT
different
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.run