require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
3 8

IN1

e.push <<EXP1
Yes

EXP1

# [2]---------------------
i.push <<IN2
2 100

IN2

e.push <<EXP2
No

EXP2

# [3]---------------------
i.push <<IN3
1 2

IN3

e.push <<EXP3
Yes

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