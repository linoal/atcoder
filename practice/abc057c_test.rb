require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'abc057c.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
10000

IN1

e.push <<EXP1
3

EXP1

# [2]---------------------
i.push <<IN2
1000003

IN2

e.push <<EXP2
7

EXP2

# [3]---------------------
i.push <<IN3
9876543210

IN3

e.push <<EXP3
6

EXP3

# [4]---------------------
# i.push <<IN4
# expect3
# IN4

# e.push <<EXP4
# expect4
# EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index].chomp(''), e[index].chomp(''))
end
t.run