require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
5
1 3 4 5 7

IN1

e.push <<EXP1
2

EXP1

# [2]---------------------
i.push <<IN2
15
13 76 46 15 50 98 93 77 31 43 84 90 6 24 14

IN2

e.push <<EXP2
3

EXP2

# [3]---------------------
i.push <<IN3
1
1
IN3

e.push <<EXP3
1
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