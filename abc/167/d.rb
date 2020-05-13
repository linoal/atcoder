
def func
    n,k = gets.split(' ').map(&:to_i)
    a = [-1] + (gets.split(' ').map(&:to_i))
    went_flags = Array.new(n,false)

    k=k+1 #移動数でなく行った街数

    path = [1]
    cnt = 0
    while true do
        

        dest = a[path.last]
        if went_flags[dest]
            break
        end
        went_flags[dest] = true
        path << dest

        # cnt += 1
        # if cnt == k
        #     return path.last.to_s
        # end
        

    end

    loop_start_town = a[path.last]
    loop_start_index = path.index(loop_start_town)
    loop_length = path.size - loop_start_index
    loop_num = (k - loop_start_index) / loop_length
    loop_mod = (k - loop_start_index - 1) % loop_length

    # puts "d:#{path}"
    # puts "d:loop_start_index: #{loop_start_index}"
    # puts "d:loop_length: #{loop_length}"
    #  puts "d:loop_num: #{loop_num}"
    #  puts "d:loop_mod: #{loop_mod}"

    if k-1 <= loop_start_index
        # puts "d:hi"
        return path[k-1].to_s
    else
        return (path[loop_start_index + loop_mod] ).to_s
    end
end

puts func