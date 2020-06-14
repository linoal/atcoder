# 素因数分解する。戻り地は2次元配列で
# [ [因数1, 指数1], [因数2, 指数2], ... ] となる。
def prime_division(arg_n)
    n = arg_n
    division = []
    i = 2
    while( i * i <= arg_n ) do
        exp = 0
        while( n % i == 0 ) do
            n /= i
            exp += 1
        end
        division.push [i, exp] if exp >= 1
        i += 1
    end
    if n != 1
        division.push [n, 1]
    end
    division
end


# オイラーのφ関数。1からnまでの自然数のうち
# nと互いの素なものの個数。
require 'prime'
def eular_phi(n)
    primes = n.prime_division
    # n が p1^e1・p2^e2 … pk^ek と素因数分解できるとき、
    # φ(n) = N ( 1 - 1/p1 ) ( 2 - 1/p2 ) … ( 1 - 1/pk )
    ret = n
    primes.each do |prime|
        ret *= prime.first - 1
        ret /= prime.first
    end
    ret
end


N = gets.to_i
puts eular_phi(N)