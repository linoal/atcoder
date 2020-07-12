require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'a.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
5 10 2

IN1

e.push <<EXP1
3

EXP1

# [2]---------------------
i.push <<IN2
6 20 7

IN2

e.push <<EXP2
2

EXP2

# [3]---------------------
i.push <<IN3
1 100 1

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