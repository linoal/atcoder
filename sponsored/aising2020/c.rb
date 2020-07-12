N = gets.to_i
max = 100
maxN = 10**4

cnts = Array.new(maxN+1, 0)
1.upto(max) do |x|
    1.upto(max) do |y|
        1.upto(max) do |z|
            n = x**2 + y**2 + z**2 + x*y + y*z + z*x
            if n <= maxN
                cnts[n] += 1
            end
        end
    end
end

1.upto(N) do |i|
    puts cnts[i]
end