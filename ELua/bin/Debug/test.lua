function test()
	return 1,2,3
end

function test2(a,b,c)
	print(a,b,c)
end

test2(test())

local t = {test()}
for i,v in ipairs(t) do
	print(i,v)
end
