a = 100
b = 200
local c = 300

local t = {a,b,c}
for i,v in ipairs(t) do
	print(i,v)
end

print("----------------------")
t[5] = 500
for i,v in ipairs(t) do
	print(i,v)
end

print("----------------------")
t[4] = 400
for i,v in ipairs(t) do
	print(i,v)
end

