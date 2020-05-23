# 模範解答 https://atcoder.jp/contests/past201912-open/submissions/13467522

n,q = gets.chomp.split(' ').map(&:to_i)
f = Array.new(n){ Array.new(n){'N'} }
q.times do
    s = gets.split(' ').map(&:to_i)
    if s[0] == 1
        f[s[1]-1][s[2]-1] = 'Y'
    elsif s[0] == 2
        for i in 0...n do
            fi = f[i][s[1]-1]
            f[s[1]-1][i] = 'Y' if fi=='Y'
        end
    elsif s[0] == 3
        stack = f[s[1]-1].each_index.select{ |i| f[s[1]-1][i]=='Y'}
        while !stack.empty? do
            fl = stack.pop
            for i in 0...n do
                fli = f[fl][i]
                f[s[1]-1][i] = 'Y' if fli == 'Y' && s[1]-1 != i
            end
        end
    end
end
f.each do |fi|
    fi.each do |fj|
        print fj
    end
    print "\n"
end

