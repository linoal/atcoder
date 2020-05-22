require "~/atcoder/lib/test"
t = Test.new('./a.rb')

in1 = <<~TEXT
678
TEXT

in2 = <<~TEXT
abc
TEXT

in3 = <<~TEXT
0x8
TEXT

t.add(in1, '1356')
t.add(in2, 'error')
t.add(in3, 'error')
t.add('012','24')
t.run