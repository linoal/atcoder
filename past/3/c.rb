a,r,n = gets.split(' ').map(&:to_i)
if r==1
    puts a
else
    ans = a
    ni = 1
    while (ni != n && ans <= 10**9) do
        ans *= r
        ni += 1
    end
    if ans > 10**9
        puts 'large'
    else
        puts ans
    end
end