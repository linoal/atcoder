require 'prime'

N = gets.to_i
factors = N.prime_division

num = 0
factors.each do |prime|
    exp = prime[1]
    next if exp == 0
    s = 0
    for i in 1..9999999
        s += i
        # puts prime[0].to_s << "^" << i.to_s
        if s > exp
            break
        end
        num += 1
    end
end

puts num