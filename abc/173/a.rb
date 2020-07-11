N = gets.to_i
if N % 1000 == 0
    puts "0"
else
    puts 1000-(N%1000)
end