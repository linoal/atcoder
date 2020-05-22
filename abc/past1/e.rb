n,q = gets.chomp.split(' ').map(&:to_i)
f = Array.new(n).map{ Array.new(n).map{'N'} }
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
        stack = f[s[1]-1].each_index.select{ |i| i=='Y'}
        while !stack.empty? do
            fl = stack.pop
            for i in 0...n do
                fli = f[fl][i]
                f[s[1]-1][i] = 'Y' if fli == 'Y'
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