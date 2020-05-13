# ABC086C - Traveling

n = gets.to_i
t,x,y = [[0],[0],[0]]
n.times do
    arg = gets.chomp.split(' ').map(&:to_i)
    t << arg[0]; x << arg[1]; y << arg[2]
end

result = 'Yes'
for i in 1..n
    dist = (x[i] - x[i-1]).abs + (y[i] - y[i-1]).abs
    if (t[i] - t[i-1] - dist) % 2 == 0 && dist <= t[i]-t[i-1]
        next
    else
        result = 'No'
        break
    end
end
puts result