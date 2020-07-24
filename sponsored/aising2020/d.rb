def popcount(x)
    p = 0
    while x > 0
        x &= x - 1
        p += 1
    end
    return p
end

N = gets.to_i
X = gets.to_i(2)
ans = Array.new(N,0)

p0 = 0
N.times do |i|
    p0 += 1 if X[i] == 1
end


x0_mod_pp1 = X % (p0+1)
x0_mod_pm1 = X % (p0-1) if p0 != 1

N.times do |i|
    next if X ^ (1 << i) == 0
    pi = p0
    r = 0
    if X[i] == 1
        pi -= 1
        r = (x0_mod_pm1 - 2.pow(i, pi) + pi) % pi
        ans[i] += 1
    else
        pi += 1
        r = (x0_mod_pp1 + 2.pow(i, pi)) % pi
        ans[i] += 1
    end
    
    # r = xi
    # p r
    while r != 0 do
        pi = popcount(r)
        r = r % pi
        ans[i] += 1
    end
end
puts ans.reverse