CON_TYPE = 26
# 問題文の入力
D = gets.to_i
C = gets.split.map(&:to_i)
S = []
D.times do |d|
    S.push(Array.new(gets.split.map(&:to_i)))
end

# 回答の入力
T = []
D.times do |d|
    T.push (gets.to_i - 1)
end

# 得点計算
sat = 0
lasts = Array.new(CON_TYPE, -1)
D.times do |d|
    CON_TYPE.times do |type|
        if(T[d] == type)
            sat += S[d][type]
            lasts[type] = d
            # puts "#{type.to_s} held. sat+=#{S[d][type]}"
        else
            sat -= C[type] * (d - lasts[type])
            # puts "sat-=#{C[type] * (d - lasts[type])}"
        end
    end
    puts sat
end