gets
a = gets.chomp.split(' ').map(&:to_i)

cnt = 0
while a.all? { |ai| ai%2==0 }
    a = a.map{ |ai| ai/2 }
    cnt += 1
end

puts cnt