require 'prime'

N = gets.to_i
sum = 0
N.times do |i|
    k = i+1
    if k == 1
        fk = 1
    else
        e = Prime.prime_division(k).map{|p,e| e+1}
        # p e
        fk = e.inject(:*)
    end
    sum += k * fk
end
puts sum