require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'c.rb')

in1 = <<~TEXT
2 3 4
TEXT

exp1 = <<~TEXT
54
TEXT

in2 = <<~TEXT
4 3 21
TEXT

exp2 = <<~TEXT
large
TEXT

in3 = <<~TEXT
12 34 5
TEXT

exp3 = <<~TEXT
16036032
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.run