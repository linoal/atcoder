require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd2.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
5
24 11 8 3 16

IN1

e.push <<EXP1
3

EXP1

# [2]---------------------
i.push <<IN2
4
5 5 5 5

IN2

e.push <<EXP2
0

EXP2

# [3]---------------------
i.push <<IN3
10
33 18 45 28 8 19 89 86 2 4

IN3

e.push <<EXP3
5

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