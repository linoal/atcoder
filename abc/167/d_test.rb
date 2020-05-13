require "~/atcoder/lib/test"
t = Test.new('./d.rb')

in1 = <<~TEXT
4 5
3 2 4 1
TEXT

in2 = <<~TEXT
6 727202214173249351
6 5 2 5 3 2
TEXT

in3 = <<~TEXT
6 1
6 5 2 5 3 2
TEXT

t.add(in1, '4')
t.add(in2, '2')
t.add(in3, '6')
t.add("3 2\n2, 3, 1", '3')
t.add("2 1000000\n1,1", '1')
t.add("6 1\n6 5 2 5 3 2", '6')
t.add("6 2\n6 5 2 5 3 2", '2')
t.add("6 3\n6 5 2 5 3 2", '5')
t.add("6 4\n6 5 2 5 3 2", '3')
t.run