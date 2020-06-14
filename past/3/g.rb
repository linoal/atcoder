def should_search?(pos, current_dist, board)
    x,y = pos[1], pos[0]
    unless in_range?(x,y)
        return false
    end
    b = board[y][x]
    # p ( b >= 0 && b > current_dist ) || b==FIELD
    ( b >= 0 && b > current_dist+1 ) || b==FIELD
end

def in_range?(x_,y_)
    ret = 0 <= x_ && x_ < BOARD_X && 0 <= y_ && y_ < BOARD_Y
    # p "#{x}, #{y} is in_range? : #{ret}"
    ret
end

def print_board(board, queue)
    puts "-------"
    puts "queue: #{queue}"
    for yi in 0...BOARD_Y
        for xi in 0...BOARD_X
            print board[yi][xi].to_s.rjust(2) << ' '
        end
        print "\n"
    end
    dummy = gets
end

obs_num, goal_x, goal_y = gets.split(' ').map(&:to_i)
FIELD, OBSTACLE, START, GOAL = -3,-1,0,-2
CENTER_X, CENTER_Y = 201, 201
# CENTER_X, CENTER_Y = 4, 4
BOARD_X, BOARD_Y = CENTER_X*2+1, CENTER_Y*2+1
board = Array.new(BOARD_Y,FIELD).map{Array.new(BOARD_X,FIELD)}
board[CENTER_Y][CENTER_X] = START
board[goal_y+CENTER_Y][goal_x+CENTER_X] = GOAL
obs_num.times do
    obs = gets.split(' ').map(&:to_i)
    board[obs[1]+CENTER_Y][obs[0]+CENTER_X] = OBSTACLE
end

queue = [[CENTER_Y,CENTER_X]]
while(! queue.empty?) do
    # p queue
    pos = queue.shift
    x,y = pos[1], pos[0]
    # p pos
    for yi in -1..1 do
        for xi in -1..1 do
            next if (yi==-1 && xi==-1) || (yi==-1 && xi==1 ) || (xi==0 && yi==0)
            next unless in_range?(x+xi, y+yi)
            
            # p "#{x+xi}, #{y+yi}"
            if board[y+yi][x+xi] == GOAL
                puts board[y][x]+1
                exit
            end
            if should_search?([y+yi,x+xi], board[y][x], board)
                board[y+yi][x+xi] = board[y][x]+1
                queue.push [y+yi,x+xi]
            end
        end
    end
    # print_board(board, queue)
end
puts -1