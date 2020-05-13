require "~/atcoder/lib/test"
t = Test.new('./b.rb')
t.add('2 1 1 3', '2')
t.add('1 2 3 4', '0')
t.add('2000000000 0 0 2000000000', '2000000000')
t.run