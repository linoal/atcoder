require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'abc052c.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
3

IN1

e.push <<EXP1
4

EXP1

# [2]---------------------
i.push <<IN2
6

IN2

e.push <<EXP2
30

EXP2

# [3]---------------------
i.push <<IN3
1000

IN3

e.push <<EXP3
972926972

EXP3

# [4]---------------------
i.push <<IN4
8
IN4

e.push <<EXP4
96
EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index].chomp(''), e[index].chomp(''))
end
t.run