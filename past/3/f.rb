require 'set'

length_n = gets.to_i
matrix = []
length_n.times do
    matrix.push gets.chomp.chars
end

# p matrix

length_n.times do |i|
    opposite_chars = Set.new(matrix[length_n - i - 1])
    length_n.times do |j|

        char = matrix[i][j]
        exist_opposite = (opposite_chars.include?(char))

        unless exist_opposite
            matrix[i][j] = '-'
        end

    end
end

ans = ''
((length_n+1) / 2).times do |i|
    
    length_n.times do |j|
        if matrix[i][j] != '-'
            ans << matrix[i][j]
            break
        end
        if j==length_n-1
            puts '-1'
            exit
        end
    end
end

# p ans

(length_n/2).times do |i|
    ans << ans[length_n/2 - i - 1]
end

puts ans