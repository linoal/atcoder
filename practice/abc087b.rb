a,b,c,x = [gets,gets,gets,gets].map(&:to_i)
cnt = 0
for ai in 0..a
    for bi in 0..b
        for ci in 0..c
            cnt += 1 if ai*500+bi*100+ci*50 == x
        end
    end
end
puts cnt