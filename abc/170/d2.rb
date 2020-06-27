N = gets.to_i
A = gets.split.map(&:to_i).sort
MAX = 10**6
E = [0] * (MAX+1)

A.each do |a|
    am = a
    if E[am] >= 1
        E[am] += 1
        next
    end
    while am <= MAX do
        E[am] += 1
        am += a
    end
end

cnt = 0
A.each do |a|
    if E[a] == 1
        cnt += 1
    end
end
puts cnt