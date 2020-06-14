# require 'prime'
# N = gets.to_i
# puts (N.prime? ? "YES" : "NO")

def prime?( n )
    i = 2
    while i*i <= n do
        return false if n % i == 0
        i += 1
    end
    true
end

N = gets.to_i
puts (prime?(N) ? "YES": "NO")