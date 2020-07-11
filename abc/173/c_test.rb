require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'c.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
2 3 2
..#
###

IN1

e.push <<EXP1
5

EXP1

# [2]---------------------
i.push <<IN2
2 3 4
..#
###

IN2

e.push <<EXP2
1

EXP2

# [3]---------------------
i.push <<IN3
2 2 3
##
##

IN3

e.push <<EXP3
0

EXP3

# [4]---------------------
i.push <<IN4
6 6 8
..##..
.#..#.
#....#
######
#....#
#....#

IN4

e.push <<EXP4
208

EXP4

# ------------------------
i.size.times do |index|
    t.add(i[index], e[index])
end
t.run