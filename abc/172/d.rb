N = gets.to_i
ans = 0
N.times do |i|
    d = i + 1
    tn = N / d
    ans += tn * (tn+1) * d / 2
end
puts ans