local t = {1,2,3}

if true then
	for i,v in ipairs(t) do
		print(i,v)
	end
else
	print(100)
end
