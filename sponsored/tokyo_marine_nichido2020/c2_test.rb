require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'c2.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
5 1
1 0 0 1 0

IN1

e.push <<EXP1
1 2 2 1 2 

EXP1

# [2]---------------------
i.push <<IN2
5 2
1 0 0 1 0

IN2

e.push <<EXP2
3 3 4 4 3 

EXP2

# [3]---------------------
i.push <<IN3
5 9999999
0 0 0 0 0
IN3

e.push <<EXP3
5 5 5 5 5
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