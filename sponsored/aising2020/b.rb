N = gets.to_i
A = gets.split.map(&:to_i)
ans = 0
N.times do |n|
    if (n+1) % 2 == 1 && A[n] % 2 == 1
        ans += 1
    end
end
puts ans