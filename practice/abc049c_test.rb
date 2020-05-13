# require "~/atcoder/lib/test"

# t = Test.new("./abc049c.rb")
# t.add('erasedream', 'YES')
# t.add('dreameraser', 'YES')
# t.add('dreamerer', 'NO')
# t.add('dreameraseeraser','YES')
# t.add('eraserdreamer', 'YES')
# t.run

require "~/atcoder/lib/test"
t = Test.new('./abc049c.rb')
t.add('erasedream', 'YES')
t.add('dreameraser', 'YES')
t.add('dreameraseeraser', 'YES')
t.add('dreameraseeraser', 'YES')
t.add('eraserdreamer', 'YES')
t.run



# rb = "./abc049c.rb"
# Test.test( rb, 'erasedream', 'YES')
# Test.test( rb, 'dreameraser', 'YES')
# Test.test( rb, 'dreamerer', 'NO')
# Test.test( rb, 'dreameraseeraser','YES')
# Test.test( rb, 'eraserdreamer', 'YES')