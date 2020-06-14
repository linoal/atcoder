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

# p (prime_division(gets.to_i))
N = gets.to_i
print N.to_s + ':'
divs = prime_division(N)
divs.each do |div|
    div[1].times do
        print ' ' + div[0].to_s
    end
end
print "\n"