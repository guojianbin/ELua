using NUnit.Framework;

namespace ELua.Test {

    [TestFixture]
    public class ELuaTest {

		/// <summary>
		/// syntax test
		/// </summary>
        [Test]
        public void TestCase00() {
            var file = "test.lua";
            var script = @"
f[10] = 1
local a = 100.a + b % f[10].x(100,200+1,300,f(x, a+b(""hello world""))) * (-c.y[100] - b) + (c + d) + b + (c * d) - (e / f) * (b + (c * d) - (e / f + 200))
local a = b + c + d
local a = b + (c * d) - (e / f(c))
a()
a()
a(1)
a(1,2,3)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
            parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
        }

		/// <summary>
		/// test plus
		/// </summary>
        [Test]
        public void TestCase01() {
            var file = "test.lua";
            var script = @"
local b = 1
local c = 2
local a = b + c
return a
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
            parser.Generate();
			Assert.IsTrue(((LuaNumber)vm.Call(file)).value == 3);
			Assert.IsTrue(parser.errorList.Count == 0);
        }

		/// <summary>
		/// test table
		/// </summary>
        [Test]
        public void TestCase02() {
            var file = "test.lua";
            var script = @"
b = {[1]=2,[""2""]=3}
a = {a=100,b=200,c=300}
a = {1,21,33}
a = {}
print(""hello world"")
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			vm.Call(file);
        }

		/// <summary>
		/// test math
		/// </summary>
        [Test]
        public void TestCase03() {
            var file = "test.lua";
            var script = @"
a = 100 + 20 + 30
local a  = 5  - 2
print(a)
local b = 2 * 20
print(b)
local c = -100
print(c)
print(2 + 100)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test equal
		/// </summary>
        [Test]
        public void TestCase04() {
            var file = "test.lua";
            var script = @"
local a = ""10""
local b = ""10""
if a == b then
	print(b)
end
print(a)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test table property
		/// </summary>
        [Test]
        public void TestCase05() {
            var file = "test.lua";
            var script = @"
local b = {a=100,b=200,c=200,b=300}
local aa = ""a""
print(b[aa])

b.a = 500
print(b.a)
print(b.a  - 200)
print(b.c+b.a*(b.b+100))
local a = 200
print(a)
print(b[""a""])
local b = {1,2,3}
print(b[1])
print(b[2])
print(b[3])
b[1] = 100
print(b[1])
local c = nil
print(c)

local a = {a=1}
print(a.a)
a.a = 100
print(a.a)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test negate
		/// </summary>
        [Test]
        public void TestCase06() {
            var file = "test.lua";
            var script = @"
local a = 100
local b = 200
local c = a + -b
print(c)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test cutting exp
		/// </summary>
        [Test]
        public void TestCase07() {
            var file = "test.lua";
            var script = @"
local b = 10 + 20
if b  then 
	local a = 100 + 200
	print(a)
else
	local a = 200 + 400
	print(a)
end

local b = 100
local a = 1
local c = 5
print(a and 50 or 80)
print(b and b + 100 and c + 200)

local b = true
if b or a + 100 then
	if b then
		print(1)
	else
		print(5)
	end
	if a then
		print(2)
	else
		print(3)
	end
else
	print(4)
end

local a = 10
local b = 20
if a + b then
	print(a + b)
end

local a = a or 100
local b = a or 200
print(a)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test upvalue
		/// </summary>
        [Test]
        public void TestCase08() {
            var file = "test.lua";
            var script = @"
local c = 200

function test2(a,b)
	print(a + b)
	print(c)
	print(trace())
	return a * 2
end

local test3 = function(a,b)
	print(a + b)
	print(c)
	print(trace())
	return a * 2
end

test3(10,20)
local b1 = b
local b2 = b
if b1 == b2 then
	print(""haha"")
end

local t2 = test2
local t3 = test2

t2(1,2)

if t2 == t3 then
	print(""haha2"")
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test recurse
		/// </summary>
        [Test]
        public void TestCase09() {
            var file = "test.lua";
            var script = @"
b = {function(a,b,c,d)
	print(1)
end,function()
	print(2)
end,function()
	print(3)
end}

function test2(a)
	if a > 5 then return end
	print(a)
	a = a + 1
	test2(a)
end

test2(1)

b = {b={b={b={b={b=function()
	print(""haha"")
end}}}}}
b.b.b.b.b.b()
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test dict
		/// </summary>
        [Test]
        public void TestCase10() {
            var file = "test.lua";
            var script = @"
function test3()
	local a = function ()
		return 100,200
	end
	local b = function()
		return 200
	end
	local c = function()
		return 300
	end
	return {a,b,c}
end

local arr = test3()
for i = 1, 3 do
	print(arr[i]())
end

function test3()
	local a = function ()
		return 100
	end
	return {a}
end

print(test3()[1]())

arr[1] = 100
arr[2] = 200
arr[3] = 300

for i = 1, 3 do
	print(arr[i])
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test unpack
		/// </summary>
        [Test]
        public void TestCase11() {
            var file = "test.lua";
            var script = @"
function unpack(list, i)
	if i == nil then
		return unpack(list, 1)
	else
		if i <= len(list) then
			return list[i], unpack(list, i + 1)
		end
	end
end

print(unpack({1,2,3}))

function test1()
	return 4,5
end

function test2()
	return 1,2,3,test1()
end

print(test2())
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test define N
		/// </summary>
        [Test]
        public void TestCase12() {
            var file = "test.lua";
            var script = @"
function test1()
	return 1,2,3
end
function test2()
	return 4,5
end
local a,b,c,d,e = test1(),test2()
print(a,b,c,d,e)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test swap
		/// </summary>
        [Test]
        public void TestCase13() {
            var file = "test.lua";
            var script = @"
local a,b = 1,2
print(a,b)

arr = {3,4}
for i = 1, len(arr) do
	print(arr[i])
end

arr[1],arr[2] = a,b

for i = 1, len(arr) do
	print(arr[i])
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test while
		/// </summary>
        [Test]
        public void TestCase14() {
            var file = "test.lua";
            var script = @"
local arr = {1,2,3}
local i = 1
while i <= len(arr) do 
	for j=1,10 do
		print(j + 10)
		if j == 2 then break end
	end
	print(arr[i])
	i = i + 1
	if i == 3 then break end
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test foreach
		/// </summary>
        [Test]
        public void TestCase15() {
            var file = "test.lua";
            var script = @"
local arr = {11,22,33}

function iter(arr, i)
	i = i + 1
	local v = arr[i]
	if v then
		return i, v
	end
end

function ipairs(arr)
	return iter, arr, 0
end

for i,v in ipairs(arr) do
	print(i,v)
end

function values(tb)
     local i = 0
     return function ()
          i = i + 1
          return tb[i]
     end
end

local testTb = {10, 20, 30}
for value in values(testTb) do
     print(value)
end

testTb[1] = 100
testTb[2] = 200
testTb[3] = 300

for value in values(testTb) do
     print(value)
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test next
		/// </summary>
        [Test]
        public void TestCase16() {
            var file = "test.lua";
            var script = @"
local t = {11,22,33}
for k, v in next, t do
	print(k,v)
end

t[1] = 100
t[-1] = 200
t.a = 11
print(""-------------"")

for k, v in next, t do
	print(k,v)
end
print(""-------------"")

local k,v = next(t)
print(k,v)
k,v = next(t, k)
print(k,v)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            vm.Call(file);
        }

		/// <summary>
		/// test concat
		/// </summary>
        [Test]
        public void TestCase17() {
            var file = "test.lua";
            var script = @"
local a = ""12""
local b = ""23""
return a .. b
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			Assert.IsTrue(vm.Call(file).ToString() == "1223");
        }

		/// <summary>
		/// test pairs ipairs
		/// </summary>
        [Test]
        public void TestCase18() {
            var file = "test.lua";
            var script = @"
local a = ""12""
local b = ""23""
print(a .. b)

local t={a,b}
for k, v in pairs(t) do
	print(k,v)
end

for k, v in ipairs(t) do
	print(k,v)
end

local t={a=11,b=22}
for k, v in pairs(t) do
	print(k,v)
end

for k, v in ipairs(t) do
	print(k,v)
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			vm.Call(file);
        }

		/// <summary>
		/// test table list index
		/// </summary>
        [Test]
        public void TestCase19() {
            var file = "test.lua";
            var script = @"
a = 100
b = 200
local c = 300

local t = {a,b,c}
for i,v in ipairs(t) do
	print(i,v)
end

print(""----------------------"")
t[5] = 500
for i,v in ipairs(t) do
	print(i,v)
end

print(""----------------------"")
t[4] = 400
for i,v in ipairs(t) do
	print(i,v)
end
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			vm.Call(file);
        }

		/// <summary>
		/// test len(t)
		/// </summary>
        [Test]
        public void TestCase20() {
            var file = "test.lua";
            var script = @"
a = 100
b = 200
local c = 300

local t = {a,b,c}
for i,v in ipairs(t) do
	print(i,v)
end

print(""----------------------"")
t[5] = 500
for i,v in ipairs(t) do
	print(i,v)
end

print(""----------------------"")
t[4] = 400
for i,v in ipairs(t) do
	print(i,v)
end

print(""----------------------"")
for k, v in pairs(t) do
	print(k,v)
end

print(""t.len = "" .. #t)
return #t
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			Assert.AreEqual(vm.Call(file).ToString(), "5");
        }

		/// <summary>
		/// test fab
		/// </summary>
        [Test]
        public void TestCase21() {
            var file = "test.lua";
            var script = @"
function fab(n)
	if n <= 1 then
		return n
	else
		return n + fab(n - 1)
	end
end

return fab(10000)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			Assert.AreEqual(vm.Call(file).ToString(), "5.00029E+07");
        }

		/// <summary>
		/// test metatable
		/// </summary>
        [Test]
        public void TestCase22() {
            var file = "test.lua";
            var script = @"
local t = {}
local mt = { 1,2,3 }
setmetatable(t, mt)
return getmetatable(t)
";
            var vm = new LVM(new Logger());
            var scanner = new Scanner(file, script);
            var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			Assert.AreEqual(vm.Call(file).ToString(), "{ 1, 2, 3 }");
        }

		/// <summary>
		/// test table static function
		/// </summary>
		[Test]
		public void TestCase23() {
			var file = "test.lua";
			var script = @"
local t = {}

function t.test()
	print(100)
end

function t.test2(i)
	print(i)
end

t.test()
t.test2(200)
";
			var vm = new LVM(new Logger());
			var scanner = new Scanner(file, script);
			var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			vm.Call(file);
		}

		/// <summary>
		/// test metatable __add
		/// </summary>
		[Test]
		public void TestCase24() {
			var file = "test.lua";
			var script = @"

local t1 = {1,2,3}
table.insert(t1, 2, 10)
local t2 = {4,5,6}

local mt = {}
mt.__add = function(t1, t2)
	local t = {}
	for i, v in ipairs(t1) do
		table.insert(t, v)
	end
	for i, v in ipairs(t2) do
		table.insert(t, v)
	end
	return t
end

setmetatable(t1, mt)
setmetatable(t2, mt)

local t = t1 + t2
print(#t)
for i, v in ipairs(t) do
	print(i,v)
end
";
			var vm = new LVM(new Logger());
			var scanner = new Scanner(file, script);
			var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
			vm.Call(file);
		}

		/// <summary>
		/// test while cond
		/// </summary>
		[Test]
		public void TestCase25() {
			var file = "test.lua";
			var script = @"
function test()
	return 1,2,3
end

function test2(a,b,c)
	print(a,b,c)
	print(b^c)
end

test2(test())

local t = {test()}
for i,v in ipairs(t) do
	print(i,v)
end

b = {b={b={b={b={b=function()
	print(100)
end}}}}}
b.b.b.b.b.b()

b.b.b.b.b.b = 200
print(b.b.b.b.b.b)

local t = {t={t=100}}
print(t.t.t)
t.t.t =200
print(t.t.t)

local t = {t={}}

while #t.t < 5 do 
	table.insert(t.t, 10)
	print(#t.t)
end

s = """"
for i,v in ipairs(t.t) do
	-- print(i,v)
	s = s .. v
end

print(s)
return s
";
			var vm = new LVM(new Logger());
			var scanner = new Scanner(file, script);
			var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
			Assert.IsTrue(parser.errorList.Count == 0);
            Assert.AreEqual(vm.Call(file).ToString(), "1010101010");
        }

    }

}