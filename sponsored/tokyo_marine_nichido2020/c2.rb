N, K = gets.split.map(&:to_i)
a = gets.split.map(&:to_i)

if K > 40
    puts ([N]*N).join(' ')
    exit
end

[K, 40].min.times do |k|
    imosu = [0] * N
    0.upto(N-1) do |i|
        l = [i - a[i], 0].max
        r = i + a[i] + 1
        # l = i - a[i]
        # l = 0 if l < 0
        # r = i + a[i] + 1
        # r = N if r > N
        imosu[l] += 1
        imosu[r] += -1 if r < N
    end
    s = 0
    # all_n = true
    0.upto(N-1) do |i|
        s += imosu[i]
        a[i] = s
        # all_n = false if a[i] != N
    end
    # break if all_n
end

puts a.join(' ')