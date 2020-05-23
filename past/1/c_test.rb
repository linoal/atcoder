require "~/atcoder/lib/test"
t = Test.new('./c.rb')

in1 = <<~TEXT
4 18 25 20 9 13
TEXT

in2 = <<~TEXT
95 96 97 98 99 100
TEXT

in3 = <<~TEXT
19 92 3 35 78 1
TEXT

t.add(in1, '18')
t.add(in2, '98')
t.add(in3, '35')
t.run