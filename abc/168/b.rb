k = gets.chomp.to_i
s = gets.chomp

if s.size <= k
    puts s
else
    puts s[0...k] + '...'
end