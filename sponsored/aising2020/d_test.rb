require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'd.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
3
011

IN1

e.push <<EXP1
2
1
1
EXP1


i.push <<INA
3
100
INA
#ああ
e.push <<EXPA
0
1
2
EXPA



# [2]---------------------
i.push <<IN2
23
00110111001011011001110

IN2

e.push <<EXP2
2
1
2
2
1
2
2
2
2
2
2
2
2
2
2
2
2
2
2
2
2
1
3

EXP2

# [3]---------------------
s3 = ("1001011110")
s32 = s3 * 2 * 10**4
n3 = s32.length
i.push (n3.to_s + "\n" + s32)

e.push <<EXP3
??
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