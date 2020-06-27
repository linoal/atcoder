N = gets.to_i
A = gets.split.map(&:to_i)

sum = 0
N.times do |i|
    sum = sum ^ A[i]
end

ans = []
N.times do |i|
    ans.push sum ^ A[i]
end
puts ans.join(' ')