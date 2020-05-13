n,m,x = gets.split(' ').map(&:to_i)
c = Array.new(n)
a = Array.new(n).map{ Array.new(m) }

n.times do |i|
    args = gets.chomp.split(' ').map(&:to_i)
    c[i] = args[0]
    m.times do |j|
        a[i][j] = args[j+1]
    end
end

bignum = 9999999999
min_price = bignum

(2 ** n).times do |bk_bit|
    price = 0
    skills = Array.new(m, 0)
    n.times do |i|
        if bk_bit[i] != 0
            m.times do |j|
                skills[j] += a[i][j]
            end
            price += c[i]
        end
    end

    if skills.all?{ |s| s >= x } && price < min_price
        min_price = price
    end
end

min_price == bignum ? (puts '-1') : (puts min_price.to_s)