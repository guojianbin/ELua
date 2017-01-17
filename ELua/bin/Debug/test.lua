local t = {}
local mt = { 1,2,3 }
setmetatable(t, mt)
print(getmetatable(t))