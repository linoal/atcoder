n = gets.to_i
name = ''
while true
    name += (('a'.ord) + ((n-1) % 26)  ).chr
    break if n == 26
    n = (n-1)/26
    break if n<=0
    # puts n ; gets;
end
puts name.reverse
