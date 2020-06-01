IN = gets.split
A = IN[0].to_i
B = ((IN[1].to_f * 100)+0.001).to_i
mul = ((B*A).to_i) / 100

puts mul