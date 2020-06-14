n,m,q = gets.split(' ').map(&:to_i)
solve_cnt = Array.new(m, 0)
solve_sheet = Array.new(n){Array.new(m,false)}
q.times do
    qi = gets.split(' ').map(&:to_i)
    case qi[0]
    when 1 then
        n1 = qi[1]-1
        score = 0
        for mi in 0...m do
            next unless solve_sheet[n1][mi]
            score += (n-solve_cnt[mi])
        end
        puts score
    when 2 then
        n2,m2 = qi[1]-1, qi[2]-1
        solve_cnt[m2] += 1
        solve_sheet[n2][m2] = true
    end
end