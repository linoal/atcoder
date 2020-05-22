n = gets.to_i
a = []
n.times do
    a.push gets.to_i
end

for i in 1...n do
    d = a[i] - a[i-1]
    if d > 0
        puts "up #{d}"
    elsif d < 0
        puts "down #{-d}"
    else
        puts "stay"
    end
end