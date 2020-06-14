# nが素数か判定。Ruby標準で同機能がある。
# なぜかruby標準の Integer.prime? と比べて1/2の時間ですむ
# 計算量 O(√N)
def prime?( n )
    i = 2
    while i*i <= n do
        return false if n % i == 0
        i += 1
    end
    true
end


# 約数を列挙する。配列形式で返す。
# コンテスト問題ではまだテストしてない。
# 計算量 O(√N)
def divisors(n)
    ret = []
    i = 1
    while i*i <= n do
        if n % i == 0
            ret.push i
            ret.push n/i unless i*i == n
        end
        i += 1
    end
    ret.sort
end

# nの桁数を数える。 n<=0のときは0を返す。
# 十分軽量。
def digits_cnt(n)
    cnt = 0
    ni = n
    while ni > 0 do
        ni /= 10
        cnt += 1
    end
    return cnt
end

# Ruby標準にも同機能がある。素因数分解する。戻り地は2次元配列で
# [ [因数1, 指数1], [因数2, 指数2], ... ] となる。
# 計算量 O(√N)
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