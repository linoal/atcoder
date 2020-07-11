N = gets.to_i
A = gets.split.map(&:to_i).sort.reverse
sum = 0
(N-1).times do |k|
    sum += A[(k+1)/2]
end
puts sum