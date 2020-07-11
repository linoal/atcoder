require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'b.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
6
AC
TLE
AC
AC
WA
TLE

IN1

e.push <<EXP1
AC x 3
WA x 1
TLE x 2
RE x 0

EXP1

# [2]---------------------
i.push <<IN2
10
AC
AC
AC
AC
AC
AC
AC
AC
AC
AC

IN2

e.push <<EXP2
AC x 10
WA x 0
TLE x 0
RE x 0

EXP2

# [3]---------------------
# i.push <<IN3
# input3
# IN3

# e.push <<EXP3
# expect3
# EXP3

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