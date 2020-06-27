require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
cupofcoffee
cupofhottea

IN1

e.push <<EXP1
4

EXP1

# [2]---------------------
i.push <<IN2
abcde
bcdea

IN2

e.push <<EXP2
5

EXP2

# [3]---------------------
i.push <<IN3
apple
apple

IN3

e.push <<EXP3
0

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