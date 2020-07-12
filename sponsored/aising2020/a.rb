L, R, d = gets.split.map(&:to_i)
ans = 0
L.upto R do |i|
    if i % d == 0
        ans += 1
    end
end
puts ans