namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class TypeHelper {

        public static LuaTable CreateList(StackFrame stackFrame, LuaObject[] args) {
			var table = new LuaTable { IsList = true, IsInit = true, list = new LuaList(), uid = stackFrame.vm.NewUID() };
            table.list.table = table;
            for (var i = 0; i < args.Length; i ++) {
                table.list.Add(args[i]);
            }
            return table;
        }

        public static LuaTable CreateTable(StackFrame stackFrame, LuaObject[] args) {
			var table = new LuaTable { IsList = false, IsInit = true, dict = new LuaDict(), uid = stackFrame.vm.NewUID() };
            table.dict.table = table;
            for (var i = 0; i < args.Length; i += 2) {
	            table.dict.Bind(args[i], args[i + 1]);
            }
            return table;
        }

	    public static LuaFunction CreateFunction(StackFrame stackFrame, string name, string[] argsList) {
		    var func = new LuaFunction { name = name, argsList = argsList, uid = stackFrame.vm.NewUID() };
		    return func;
	    }

    }

}