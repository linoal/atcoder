X, Y = gets.split.map(&:to_i)
ok = (2*X <= Y && Y <= 4*X && Y % 2 == 0)
puts (ok ? "Yes": "No")