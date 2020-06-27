require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'c.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
6 5
4 7 10 6 5

IN1

e.push <<EXP1
8

EXP1

# [2]---------------------
i.push <<IN2
10 5
4 7 10 6 5

IN2

e.push <<EXP2
9

EXP2

# [3]---------------------
i.push <<IN3
100 0
9999

IN3

e.push <<EXP3
100

EXP3

# [4]---------------------
# i.push <<IN4
# expect3
# IN4
# 
# e.push <<EXP4
# expect4
# EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index], e[index])
end
t.run