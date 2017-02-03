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

for i=1,10 do
	print(i)
end