n,m = gets.split(' ').map(&:to_i)
a = Array.new(m)
b = Array.new(m)
graph = Array.new(n).map{ [] }
for m_i in 0...m do
    a[m_i], b[m_i] = gets.split(' ').map(&:to_i).map{ |a| a-1 }
    graph[a[m_i]] << b[m_i]
    graph[b[m_i]] << a[m_i]
end


queue = [0]
dists = Array.new(n) ; dists[0] = 0
signs = Array.new(n)
visited = Array.new(n,false) ; visited[0] = true

while !queue.empty? do
    room = queue.shift

    graph[room].each do |r|
        if !visited[r]
            queue.push r
            signs[r] = room
            dists[r] = dists[room]+1
            visited[r] = true
        end
    end
end

puts 'Yes'
for n_i in 1...n
    puts signs[n_i]+1
end
