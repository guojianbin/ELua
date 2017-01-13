namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class TypeHelper {

        public static LuaTable CreateList(StackFrame stackFrame, LuaObject[] args) {
	        if (args.Length == 0) {
		        var table = stackFrame.vm.GetTable();
		        return table;
	        } else {
				var table = stackFrame.vm.GetTable();
				table.InitList();
				for (var i = 0; i < args.Length; i++) {
					table.Add(args[i]);
				}
				return table;
	        }
        }

        public static LuaTable CreateTable(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				var table = stackFrame.vm.GetTable();
				return table;
	        } else {
				var table = stackFrame.vm.GetTable();
				table.InitDict();
				for (var i = 0; i < args.Length; i += 2) {
					table.Bind(args[i], args[i + 1]);
				}
				return table;
			}
        }

    }

}