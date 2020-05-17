include Math

a,b,h,m = gets.split(' ').map(&:to_i)

t = 60*h+m
theta_a = 2.0 * PI * t / (60*12)
theta_b = 2.0 * PI * t / 60

puts sqrt( a**2 + b**2 - 2*a*b*cos(theta_a - theta_b) )