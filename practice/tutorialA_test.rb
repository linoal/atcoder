# IO.popen("ruby ./tutorialA.rb", 'w+') do |io|
#     io.puts '1'
#     io.puts '2 3'
#     io.puts 'test'
#     p actual = io.gets.chomp
#     p expected = '6 test'
#     p actual == expected
# end

# IO.popen("ruby ./tutorialA.rb", 'w+') do |io|
#     io.puts '72'
#     io.puts '128 256'
#     io.puts 'myonmyon'
#     p actual = io.gets.chomp
#     p expected = '456 myonmyon'
#     p actual == expected
# end

require "~/atcoder/lib/test"

path = "./tutorialA.rb"
Test.test( path, %w(1 2\ 3 test), '6 test')
Test.test( path, %w(72 128\ 256 myonmyon), '456 myonmyon')