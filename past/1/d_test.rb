# require "~/atcoder/lib/test"
# t = Test.new( __dir__ << '/' << 'd.rb' )

# in1 = <<~TEXT
# 6
# 1
# 5
# 6
# 3
# 2
# 6
# TEXT

# in2 = <<~TEXT
# 7
# 5
# 4
# 3
# 2
# 7
# 6
# 1
# TEXT

# in3 = <<~TEXT
# 6
# 1
# 2
# 2
# 3
# 5
# 6
# TEXT

# in4 = <<~TEXT
# 6
# 1
# 2
# 3
# 4
# 5
# 5
# TEXT

# # in3 = <<~TEXT
# # input3
# # TEXT

# t.add(in1, '6 4')
# t.add(in2, 'Correct')
# t.add("1\n1", 'Correct')
# t.add("2\n2\n2", "2 1")
# t.add(in3, "2 4")
# t.add(in4, "5 6")
# t.run


require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd.rb')

in1 = <<~TEXT
6
1
5
6
3
2
6
TEXT

exp1 = <<~TEXT
6 4
TEXT

in2 = <<~TEXT
7
5
4
3
2
7
6
1
TEXT

exp2 = <<~TEXT
Correct
TEXT

in3 = <<~TEXT
6
1
2
2
3
5
6
TEXT

exp3 = <<~TEXT
2 4
TEXT

t.add(in1, exp1.chomp)
t.add(in2, exp2.chomp)
t.add(in3, exp3.chomp)
t.run