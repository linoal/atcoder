require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'arg017a.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
17

IN1

e.push <<EXP1
YES

EXP1

# [2]---------------------
i.push <<IN2
18

IN2

e.push <<EXP2
NO

EXP2

# [3]---------------------
i.push <<IN3
999983

IN3

e.push <<EXP3
YES

EXP3

# [4]---------------------
i.push <<IN4
672263

IN4

e.push <<EXP4
NO

EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index].chomp(''), e[index].chomp(''))
end
t.run