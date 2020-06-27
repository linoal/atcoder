require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
4

IN1

e.push <<EXP1
23

EXP1

# [2]---------------------
i.push <<IN2
100

IN2

e.push <<EXP2
26879

EXP2

# [3]---------------------
i.push <<IN3
10000000

IN3

e.push <<EXP3
838627288460105

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