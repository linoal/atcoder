

def update_cost(n, new_cost, costs)
    if costs[n].nil? || costs[n] > new_cost
        costs[n] = new_cost
        true
    else
        false
    end
end

test_num = gets.to_i
targets_num, costs_a, costs_b, costs_c, costs_d = [], [], [], [], []
test_num.times do
    n,a,b,c,d = gets.split(' ').map(&:to_i)
    targets_num.push n
    costs_a.push a
    costs_b.push b
    costs_c.push c
    costs_d.push d
end


test_num.times do |test|
    costs = {}
    n,a,b,c,d = targets_num[test], costs_a[test], costs_b[test], costs_c[test], costs_d[test]
    queue = [[n,0]]
    while !queue.empty? do
        node = queue.shift
        n_i = node[0]
        cost_i = node[1]
        if n_i % 5 == 0
            if (n_i - n_i/5)*d < c
                if update_cost(n_i/5, cost_i+ d * (n_i-n_i/5), costs)
                    queue.push([n_i/5, cost_i + d * (n_i-n_i/5)])
                end
            else
                if update_cost(n_i/5, cost_i+c, costs)
                    queue.push( [n_i/5, cost_i + c] )
                end
            end
        end
        if n_i % 3 == 0
            if (n_i - n_i/3)*d < b
                if update_cost(n_i/3, cost_i + d * (n_i-n_i/3), costs)
                    queue.push[n_i/3, cost_i + d * (n_i-n_i/3)]
                end
            else
                if update_cost(n_i/3, cost_i + b, costs)
                    queue.push ([n_i/3, cost_i + b] )
                end
            end
        end
        if n_i % 2 == 0
            if (n_i - n_i/2)*d < a
                if update_cost(n_i/2, cost_i + d * (n_i-n_i/2), costs)
                    queue.push [n_i/2, cost_i + d * (n_i-n_i/2)]
                end
            else
                if update_cost(n_i/2, cost_i + a, costs)
                    queue.push ([n_i/2, cost_i + a] )
                end
            end
        end
        if (n_i%5!=0 && n_i%3!=0 && n_i%2!=0 && n_i >= 1 && n_i < n+5)
            queue.push ([n_i-1, cost_i + d]) if update_cost(n_i-1, cost_i+d, costs)
            queue.push ([n_i+1, cost_i + d]) if update_cost(n_i+1, cost_i+d, costs)
        end
        p queue
        p costs
        gets
    end
    puts costs[0]
end