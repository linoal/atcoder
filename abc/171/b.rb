N, K = gets.split.map(&:to_i)
P = gets.split.map(&:to_i).sort
sum = 0
K.times do |i|
    sum += P[i]
end
puts sum