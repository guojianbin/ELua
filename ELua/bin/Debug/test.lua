-- f[10] = 1
-- local a = 100.a + b % f[10].x(100,200+1,300,f(x, a+b("hello world"))) * (-c.y[100] - b) + (c + d) + b + (c * d) - (e / f) * (b + (c * d) - (e / f + 200))
-- local a = b + c + d
-- local a = b + (c * d) - (e / f(c))
-- a()
-- a()
-- a(1)
-- a(1,2,3)



------------------------------ plus
local b = 1
local c = 2
local a = b + c
print(a)



-------------------------------table
b = {[1]=2,["2"]=3}
a = {a=100,b=200,c=300}
a = {1,21,33}
a = {}
print("hello world")

--------------------------------math
a = 100 + 20 + 30
local a  = 5  - 2
print(a)
local b = 2 * 20
print(b)
local c = -100
print(c)
print(2 + 100)




---------------------------equal
local a = "10"
local b = "10"
if a == b then
	print(b)
end
print(a)



----------------------------------table
local b = {a=100,b=200,c=200,b=300}
local aa = "a"
print(b[aa])

b.a = 500
print(b.a)
print(b.a  - 200)
print(b.c+b.a*(b.b+100))
local a = 200
print(a)
print(b["a"])
local b = {1,2,3}
print(b[1])
print(b[2])
print(b[3])
b[1] = 100
print(b[1])
local c = nil
print(c)



----------------------------------negate
local a = 100
local b = 200
local c = a + -b
print(c)


--------------------------------------- 短路求值
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




-------------------------------- true false
local a = 10
local b = 20
if a + b then
	print(a + b)
end

local a = a or 100
local b = a or 200
print(a)



----------------------------------upvalue
local c = 200

function test2(a,b)
	print(a + b)
	print("hello world")
	print(c)
	print(trace())
	return a * 2
end

local test3 = function(a,b)
	print(a + b)
	print("hello world")
	print(c)
	print(trace())
	return a * 2
end

test3(10,20)

----------------------------------var
local b1 = b
local b2 = b
if b1 == b2 then
	print("haha")
end

local t2 = test2
local t3 = test2

t2(1)

if t2 == t3 then
	print("test2")
end


-----------------------rescurse
b = {function(a,b,c,d)
	print(1)
end,function()
	print(2)
end,function()
	print(3)
end}

function test2(a)
	if a > 500 then return end
	print(a)
	a = a + 1
	test2(a)
end

test2(1)



----------------------------property
b = {b={b={b={b={b=function()
	print("haha")
end}}}}}
b.b.b.b.b.b()







----------------------------dict
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


------------------ unpack -=-------------
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






-------------------defineN
function test1()
	return 1,2,3
end
function test2()
	return 4,5
end
local a,b,c,d,e = test1(),test2()
print(a,b,c,d,e)





-------------------swap
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





----------------while do

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



-------------------foreach
local arr = {1,2,3}

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

for i, v in ipairs(arr) do
	print(i,v)
end


----------------------------
local a = {a=1}
print(a.a)
a.a = 100
print(a.a)