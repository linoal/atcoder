a,b = gets.split(' ').map(&:to_i)
(a%2==0 || b%2==0) ? (puts 'Even'):(puts 'Odd')