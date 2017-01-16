namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class TableHelper {

        public static LuaTable CreateList(StackFrame stackFrame, LuaObject[] args) {
	        if (args.Length == 0) {
		        var table = stackFrame.vm.GetTable();
		        return table;
	        } else {
				var table = stackFrame.vm.GetTable();
				foreach (var item in args) {
				    table.Add(item);
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
				for (var i = 0; i < args.Length; i += 2) {
					table.Bind(args[i], args[i + 1]);
				}
				return table;
			}
        }

    }

}