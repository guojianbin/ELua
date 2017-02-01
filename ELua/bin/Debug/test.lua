-- function test()
-- 	return 1,2,3
-- end

-- function test2(a,b,c)
-- 	print(a,b,c)
-- 	print(b^c)
-- end

-- test2(test())

-- local t = {test()}
-- for i,v in ipairs(t) do
-- 	print(i,v)
-- end

-- b = {b={b={b={b={b=function()
-- 	print(100)
-- end}}}}}
-- b.b.b.b.b.b()

-- b.b.b.b.b.b = 200
-- print(b.b.b.b.b.b)

-- local t = {t={t=100}}
-- print(t.t.t)
-- t.t.t =200
-- print(t.t.t)

local t = {t={}}

while #t.t < 5 do 
	table.insert(t.t, 10)
	print(#t.t)
end

s = ""
for i,v in ipairs(t.t) do
	-- print(i,v)
	s = s .. v
end

print(s)