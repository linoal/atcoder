require "~/atcoder/lib/test"
t = Test.new('./b.rb')

in1 = <<~TEXT
7
nikoandsolstice
TEXT

in2 = <<~TEXT
40
ferelibenterhominesidquodvoluntcredunt
TEXT

in3 = <<~TEXT
1
a
TEXT



t.add(in1, 'nikoand...')
t.add(in2, 'ferelibenterhominesidquodvoluntcredunt')
t.add(in3, 'a')
t.run