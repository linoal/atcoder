N, M, K = gets.split.map(&:to_i)
A = gets.split.map(&:to_i)
B = gets.split.map(&:to_i)

# t = 0
# read = 0
# while(t < K)
#     k = -1
#     if A.empty? && B.empty?
#         break
#     elsif A.empty?
#         k = B.shift
#     elsif B.empty?
#         k = A.shift
#     elsif A[0] < B[0]
#         k = A.shift
#     elsif A[0] >= B[0]
#         k = B.shift
#     else
#         puts "something is wrong"
#     end
#     if t + k <= K
#         read += 1
#         t += k
#     else
#         break
#     end
# end
# puts read

sumA = Array.new(1,0)
s = 0
A.size.times do |i|
    s += A[i]
    sumA.push s
end
# puts sumA

sumB = []
s = 0
B.size.times do |i|
    s += B[i]
     sumB.push s
end

max_read = -1
(A.size+1).times do |i|
    if sumA[i] > K
        # puts "braked. " + sumA[i].to_s
        break
    end
    b_time = K-sumA[i]
    # print "sumA[" + i.to_s + "] : " + sumA[i].to_s + "  b_time: " + b_time.to_s + "  "
    bi = sumB.bsearch_index{|b| b > b_time}
    bread = -1
    if bi.nil?
        bread = sumB.size
    elsif bi == 0
        bread = 0
    else
        bread = bi
    end
    read = i + bread
    if read > max_read
        max_read = read
    end
    # puts "Aread:" + i.to_s + "  Bread:" + bread.to_s
end
puts max_read