H,W,K = gets.split.map(&:to_i)
C = []
num_black = 0
H.times do |h|
    C.push gets.strip
    num_black += C[-1].count('#')
end

num_to_erase = num_black - K
# puts num_to_erase

ans = 0
(2**H).times do |h|
    (2**W).times do |w|
        c = Marshal.load(Marshal.dump(C))
        erased = 0
        H.times do |i|
            if h[i] == 1
                erased += c[i].count('#')
                c[i] = 'x' * W
            end
        end
        W.times do |j|
            if w[j] == 1
                H.times do |k|
                    if c[k][j,1] == "#"
                        erased += 1
                    end
                    c[k][j,1] = "x"
                end
            end
        end

        # p c
        # puts "---- #{erased}"
        if erased == num_to_erase
            ans += 1
        end
    end
end
puts ans