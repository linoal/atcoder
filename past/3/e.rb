node_num, edge_num, query_num = gets.split(' ').map(&:to_i)
graph = Array.new(node_num){Array.new()}

edge_num.times do
    edge = gets.split(' ').map(&:to_i)
    graph[edge[0]-1].push edge[1]-1
    graph[edge[1]-1].push edge[0]-1
end
color = gets.split(' ')

query_num.times do
    query = gets.split(' ').map(&:to_i)
    case query[0]
    when 1 then
        x = query[1]-1
        puts color[x]
        graph[x].each do |node|
            color[node] = color[x]
        end
    when 2 then
        x,y = query[1]-1, query[2]
        puts color[x]
        color[x] = y
    else
        puts 'error'
    end
end