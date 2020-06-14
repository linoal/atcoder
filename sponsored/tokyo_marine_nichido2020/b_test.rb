require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
1 2
3 1
3

IN1

e.push <<EXP1
YES

EXP1

# [2]---------------------
i.push <<IN2
1 2
3 2
3

IN2

e.push <<EXP2
NO

EXP2

# [3]---------------------
i.push <<IN3
1 2
3 3
3

IN3

e.push <<EXP3
NO

EXP3

# [4]---------------------
i.push <<IN4
0 3
5 0
1
IN4

e.push <<EXP4
NO
EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index].chomp(''), e[index].chomp(''))
end
t.run