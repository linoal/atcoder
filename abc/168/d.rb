$BIGNUM = 9999999999

class Room
    attr_accessor :num, :dist, :sign
    def initialize(num) 
        @num = num
        @dist = $BIGNUM
        @sign = nil
    end
end


n,m = gets.split(' ').map(&:to_i)
rooms = Array.new(n+1).map.with_index { |r, i| Room.new(i) }
rooms[1].dist = 0
a = Array.new(m+1)
b = Array.new(m+1)
m.times do |i|
    a[i+1], b[i+1] = gets.split(' ').map(&:to_i)
end


while true
    all_signed = true
    for road_num in 1..m
        room_a = rooms[a[road_num]]
        room_b = rooms[b[road_num]]
        if room_a.dist > room_b.dist + 1
            room_a.dist = room_b.dist + 1
            room_a.sign = room_b.num
            all_signed = false
        elsif room_b.dist > room_a.dist + 1
            room_b.dist = room_a.dist + 1
            room_b.sign = room_a.num
            all_signed = false
        end

        # p room_a
        # p room_b

    end


    # for i in 2..n
    #     print rooms[i].sign; print ' '
    # end
    # print "\n"


    break if all_signed
end

puts 'Yes'
for i in 2..n
    p rooms[i].sign
end
