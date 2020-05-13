gets
a = gets.chomp.split(' ').map(&:to_i).sort!.reverse!
diff = 0
a.each_with_index do |ai, i|
    if i%2==0
        diff += ai
    else
        diff -= ai
    end
end
puts diff