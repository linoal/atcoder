def obs_in?(l1, l2, obs)
    ret = false
    obs.each do |ob|
        ret = true if l1 <= ob && ob <= l2
    end
end


obs_num, goal_length = gets.split(' ').map(&:to_i)
obs = gets.split(' ').map(&:to_i)
t1,t2,t3 = gets.split(' ').map(&:to_i)

GO_2M, GO_4M, GO_1M = 0,1,2

 speed = [] # 4mあたり
 speed.push [GO_2M, (t1+t2)*2]
 speed.push [GO_4M, t1+3*t2]
 speed.push [GO_1M, 4*t1]
 speed.sort{ |a,b| a[1] <=> b[1]}

 cpos = 0
 ctime = 0

 while cpos < goal_length do
    run_type = nil
    speed.each do |sp|
        case sp[0]
        when GO_2M then
            unless obs_in?(cpos,0.5+cpos,obs) || obs_in?(1.5+cpos,cpos+2)
                run_type = GO_2M
            end
        when GO_4M then
            unless obs_in?(cpos,0.5+cpos,obs) || obs_in?(3.5+cpos,cpos+4)
                run_type = GO_4M
            end
        else
            run_type = GO_1M
        end
        break unless run_type.nil?
    end

    case run_type
    when GO_1M then
        cpos += 1
        ctime += t1
    when GO_2M then
        cpos += 2
        ctime +=  t1+t2
    when GO_4M then
        cpos += 4
        ctime += t1+3*t2
    end
end