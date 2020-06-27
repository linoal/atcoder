require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
4
1 2 3 4
3
1 2
3 4
2 4

IN1

e.push <<EXP1
11
12
16

EXP1

# [2]---------------------
i.push <<IN2
4
1 1 1 1
3
1 2
2 1
3 5

IN2

e.push <<EXP2
8
4
4

EXP2

# [3]---------------------
i.push <<IN3
2
1 2
3
1 100
2 100
100 1000

IN3

e.push <<EXP3
102
200
2000

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