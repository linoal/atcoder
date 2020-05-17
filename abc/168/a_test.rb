require "~/atcoder/lib/test"
t = Test.new('./a.rb')

in1 = <<~TEXT
16
TEXT

in2 = <<~TEXT
2
TEXT

in3 = <<~TEXT
183
TEXT

t.add(in1, 'pon')
t.add(in2, 'hon')
t.add(in3, 'bon')
t.run