require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'a.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
2

IN1

e.push <<EXP1
14

EXP1

# [2]---------------------
i.push <<IN2
10

IN2

e.push <<EXP2
1110

EXP2

# [3]---------------------
# i.push <<IN3
# input3
# IN3

# e.push <<EXP3
# expect3
# EXP3

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