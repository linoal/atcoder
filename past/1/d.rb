# n = gets.to_i
# a = []
# n.times{ a.push gets.to_i }
# a.sort!
# a.push n+1
# correct = true
# before = -1
# after = -1
# for i in 1...n+1 do
#     if a[i-1] + 2 == a[i]
#         before = a[i]-1
#         correct=false
#         break
#     end
# end

# for i in 1...n do
#     if a[i] == a[i-1]
#         after = a[i]
#         before = 1 if before==-1
#         puts "#{after} #{before}"
#         break
#     end
# end

# puts "Correct" if correct


n=gets.to_i
cnts = Array.new(n,0)
n.times do
    a_i = gets.to_i - 1
    cnts[a_i] += 1
end
before = cnts.index(0)
after = cnts.index(2)
puts "#{after+1} #{before+1}" if before
puts "Correct" unless before