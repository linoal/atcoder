n,y = gets.split(' ').map(&:to_i)

def func(n,y)
    for s1 in 0..n
        for s2 in s1..n
            if 10000*s1 + 5000*(s2-s1) + 1000 *(n-s2) == y
                puts "#{s1} #{s2-s1} #{n-s2}"
                return
            end
        end
    end
    puts "-1 -1 -1"
end

func(n,y)