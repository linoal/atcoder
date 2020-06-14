patterns = []
patterns.push <<TEXT
###
#.#
#.#
#.#
###
TEXT

patterns.push <<TEXT
.#.
##.
.#.
.#.
###
TEXT

patterns.push <<TEXT
###
..#
###
#..
###
TEXT

patterns.push <<TEXT
###
..#
###
..#
###
TEXT

patterns.push <<TEXT
#.#
#.#
###
..#
..#
TEXT

patterns.push <<TEXT
###
#..
###
..#
###
TEXT

patterns.push <<TEXT
###
#..
###
#.#
###
TEXT

patterns.push <<TEXT
###
..#
..#
..#
..#
TEXT

patterns.push <<TEXT
###
#.#
###
#.#
###
TEXT

patterns.push <<TEXT
###
#.#
###
..#
###
TEXT

digit_n = gets.to_i
lights = Array.new(5){ Array.new(4*digit_n+1) }
5.times do |i|
    s_i = gets.chomp
    s_i.chars.each_with_index do |c,j|
        lights[i][j] = (c=='#')
    end
end

ans = ''
for digit_i in 0...digit_n do
    digit_str = ''
    5.times do |row|
        digit_str << (lights[row][4*(digit_i+1)-3] ? '#' : '.')
        digit_str << (lights[row][4*(digit_i+1)-2] ? '#' : '.')
        digit_str << (lights[row][4*(digit_i+1)-1] ? '#' : '.')
        digit_str << "\n"
    end
    10.times do |num|
        ans << num.to_s if digit_str.chomp == patterns[num].chomp
        # p 'digit_str'
        # p digit_str
        # p 'patterns[num]'
        # p patterns[num]
    end
end
puts ans