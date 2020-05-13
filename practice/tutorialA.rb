# sum = gets.to_i + gets.split(' ').inject(0){ |s, arg| s += arg.to_i }
# str = gets.chomp

a = gets.to_i
b,c = gets.chomp.split(' ').map(&:to_i)
s = gets.chomp

puts "#{a+b+c} #{s}"