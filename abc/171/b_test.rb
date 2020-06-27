require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
5 3
50 100 80 120 80

IN1

e.push <<EXP1
210

EXP1

# [2]---------------------
i.push <<IN2
1 1
1000

IN2

e.push <<EXP2
1000

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