N = gets.to_i
A = gets.split.map(&:to_i)
mul = 1

if A.index(0)
    puts '0'
    exit
end

N.times do |i|
    if mul > 10**18 / A[i]
        puts '-1'
        exit
    end
    mul *= A[i]
end
puts mul