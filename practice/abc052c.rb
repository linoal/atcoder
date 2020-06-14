require 'prime'

N = gets.to_i
factors = {}
for i in 2..N
    ifacts = i.prime_division
    ifacts.each do |fac|
        if !(factors.has_key?(fac.first))
            factors.store( fac.first, fac.last )
        else
            factors[fac.first] += fac.last
        end
    end
end

# p factors

cnt = 1
factors.each_value do |exp|
    cnt *= exp + 1
    cnt %= 10**9 + 7
end
puts cnt