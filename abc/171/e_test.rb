require "~/atcoder/lib/test"
t = Test.new(__dir__ << '/' << 'e.rb')
i,e = [], []

# [1]---------------------
i.push <<IN1
4
20 11 9 24

IN1

e.push <<EXP1
26 5 7 22

EXP1

# [2]---------------------
input2 = "200000\n" + "0 " * 200000
i.push input2

e.push ("0 " * 200000).strip


# ------------------------
i.size.times do |index|
    t.add(i[index], e[index])
end
t.run