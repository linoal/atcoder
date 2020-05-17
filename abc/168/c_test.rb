require "~/atcoder/lib/test"
t = Test.new('./c2.rb')

in1 = <<~TEXT
3 4 9 0
TEXT

in2 = <<~TEXT
3 4 10 40
TEXT

# in3 = <<~TEXT
# input3
# TEXT

t.add(in1, '5.00000000000000000000')
t.add(in2, '4.56425719433005567605')
# t.add(in3, 'expected3')
t.run