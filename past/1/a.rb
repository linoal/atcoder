s = gets.chomp()
if s.match(/[a-z]/)
    puts 'error'
else
    puts (s.to_i)*2
end