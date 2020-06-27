S = gets.chomp.chars
T = gets.chomp.chars

num = 0
S.size.times do |i|
    if S[i] != T[i]
        num += 1
    end
end
puts num