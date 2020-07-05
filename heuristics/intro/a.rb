CON_TYPE = 26
# 問題文の入力
D = gets.to_i
C = gets.split.map(&:to_i)
S = []
D.times do |d|
    S.push(Array.new(gets.split.map(&:to_i)))
end

def calc_one_day(open_type, day)
    sat_day = 0
    # puts "open_type: #{open_type}"
    CON_TYPE.times do |type|
        if(open_type == type)
            sat_day += S[day][type]
            
            # puts "sat_day: #{sat_day}"
            # puts "#{type.to_s} held. sat+=#{S[d][type]}"
        else
            sat_day -= C[type] * (day - @lasts[type])
            # puts "sat-=#{C[type] * (d - @lasts[type])}"
        end
    end
    sat_day
end

# def calc_score(arg_array)
#     opens = []
#     D.times do |d|
#         opens.push (arg_array[d].to_i - 1)
#     end

#     # 得点計算
#     sat_list = [0]
#     sat = 0
#     @lasts = Array.new(CON_TYPE, -1)
#     D.times do |d|
#         sat += calc_one_day(opens[d], d)
#         @lasts[opens[d]] = d
#         sat_list.push sat
#     end
#     sat_list[1..-1] # 最初は0
# end

# puts calc_score([1,17,13,14,13])

# 貪欲法
opens = []
scores = [0]
@lasts = Array.new(CON_TYPE, -1)
D.times do |d|
    max_sat = -99999999999
    max_type = -1
    CON_TYPE.times do |ct|
        sat = calc_one_day(ct, d )
        if max_sat < sat
            max_sat = sat
            max_type = ct
        end
    end
    @lasts[max_type] = d
    opens.push (max_type+1)
    scores.push( scores[-1] + max_sat)
end
# puts "SAMPLE: #{calc_one_day(1,0)}\t#{calc_one_day(17,1)}"##{calc_score([1,17,13,14,13])}"
# p "LASTS: #{@lasts}"
# p "SCORES: #{scores}"
# puts "ANS:"
puts opens

