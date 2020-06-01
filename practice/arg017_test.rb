require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'arg017_test.rb')

in1 = <<~TEXT
17

TEXT

exp1 = <<~TEXT
YES

TEXT

in2 = <<~TEXT
18

TEXT

exp2 = <<~TEXT
NO

TEXT

in3 = <<~TEXT
999983

TEXT

exp3 = <<~TEXT
YES

TEXT

in4 = <<~TEXT
672263

TEXT

exp4 = <<~TEXT
NO

TEXT

t.add(in1.chomp, exp1.chomp)
t.add(in2.chomp, exp2.chomp)
t.add(in3, exp3.chomp)
t.run