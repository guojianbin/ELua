function fab(n)
	if n <= 1 then
		return n
	else
		return n + fab(n - 1)
	end
end

print(fab(10000))