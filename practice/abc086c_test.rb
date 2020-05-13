require "~/atcoder/lib/test"
t = Test.new('./abc086c.rb')
t.add(['2', '3 1 2', '6 1 1'], 'Yes')
t.add(['1', '2 100 100'], 'No')
t.add(['2', '5 1 1', '100 1 1'], 'No')
t.run