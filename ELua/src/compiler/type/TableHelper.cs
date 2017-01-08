namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class TableHelper {

        public static LuaTable CreateEmpty() {
            return new LuaTable { IsInit = false };
        }

        public static LuaTable CreateList(LuaObject[] args) {
            var table = new LuaTable { IsList = true, IsInit = true, list = new LuaList() };
            table.list.table = table;
            for (var i = 0; i < args.Length; i ++) {
                table.list.Add(args[i]);
            }
            return table;
        }

        public static LuaTable CreateTable(LuaObject[] args) {
            var table = new LuaTable { IsList = false, IsInit = true, dict = new LuaDict() };
            table.dict.table = table;
            for (var i = 0; i < args.Length; i += 2) {
                table.dict[args[i]] = args[i + 1];
            }
            return table;
        }

    }

}