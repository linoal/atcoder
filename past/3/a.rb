s = gets.chomp
t = gets.chomp

if s==t
    puts 'same'
elsif s.downcase == t.downcase
    puts 'case-insensitive'
else
    puts 'different'
end