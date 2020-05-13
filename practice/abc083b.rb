n,a,b = gets.split(' ').map(&:to_i)
sum = 0
for ni in 0..n
    d_sum = ni.to_s.chars.inject(0){ |s,c| s += c.to_i }
    sum += ni if d_sum >= a && d_sum <= b
end
puts sum