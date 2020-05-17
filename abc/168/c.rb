
include Math
a,b,h,m = gets.split(' ').map(&:to_f)

w_a = 2.0 * PI / (12*60)
w_b = 2.0 * PI / 60
t = 60*h+m

x_a = a * cos(w_a * t)
y_a = a * sin(w_a * t)
x_b = b * cos(w_b * t)
y_b = b * sin(w_b * t)

# p a
# p b
# p h
# p m
# p w_a
# p w_b
# p PI
# p t
# p "xの角度: #{w_a * t * 360 / (2*PI)}"
# p "yの角度: #{w_b * t * 360 / (2*PI) % 360}"
# p x_a
# p y_a
# p x_b
# p y_b

puts sqrt((x_a-x_b)**2 + (y_a-y_b)**2)