N, K = gets.split.map(&:to_i)
a = gets.split.map(&:to_i)

# if K >= N
#     puts Array.new(N, N).join(' ')
#     exit
# end
K.times do
    b = Array.new(N,0)
    # n_cnt = 0
    # a.each_with_index do |d,i|
    #     range = (i-d)..(i+d)
    #     range.each do |j|
    #         next if j < 0 || j >= N
    #         b[j] += 1
    #         n_cnt += 1 if b[j] == N
    #     end
    # end

    [0..N/2].each do |i|
        range = (i-d)..(i+d)
        n_index = -1
        range.each do |j|
            next if j < 0 || j >= N
            b[j] += 1
            if b[j] == N
                n_index = j
                break
            end
        end
        
    end

    N.downto(N/2).each do |i|
        range = (i-d)..(i+d)
        n_index_r = -1
        range.each do |j|
            next if j < 0 || j >= N
            b[j] += 1
            if b[j] == N
                n_index_r = j
                break
            end
        end
    end

    if n_index != -1 && n_index_r != -1
        n_index...n_index_r.each do |i|
            b[i] = N
        end
    end


    a = b
    # if n_cnt == N
    #     break
    # end
end
puts a.join(' ')