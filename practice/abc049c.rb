# s = gets.chomp
# size = s.size
# ptr = 0
# while true do
#     if ptr >= size
#         puts 'YES'
#         break
#     end

#     sp10 = s[ptr, 10]
#     sp7 = s[ptr, 7]
#     sp6 = s[ptr, 6]
#     sp5 = s[ptr, 5]
#     if sp10 == 'dreamerase'
#         ptr += 5
#         next
#     elsif sp7 == 'dreamer'
#         ptr += 7
#         next
#     elsif sp6 == 'eraser'
#         ptr += 6
#         next
#     elsif sp5 == 'dream' || sp5 == 'erase'
#         ptr += 5
#         next
#     else
#         puts 'NO'
#         break
#     end
# end



s = gets.chomp.reverse
units = %w(dream dreamer erase eraser).map(&:reverse)
# p s
# p units
ptr = 0
while true do
    found_flag = units.detect do |u|
        #p "#{s[ptr, u.size]} #{u}"
        if s[ptr, u.size] == u
            ptr += u.size
            #puts "d:#{u}"
            true
        end
    end
    if !found_flag
        puts 'NO'
        break
    elsif ptr >= s.size
        puts 'YES'
        break
    end
end
