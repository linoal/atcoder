N = gets.to_i
A = gets.split.map(&:to_i)
Q = gets.to_i

nums = Array.new(10**5+1,0)
sum = 0
A.each do |a|
    sum += a
    nums[a] += 1
end

Q.times do |q|
    b, c = gets.split.map(&:to_i)
    nums[c] += nums[b]
    sum = sum - b * nums[b] + c * nums[b]
    nums[b] = 0
    puts sum
end