namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class TableHelper {

        public static LuaTable CreateEmpty() {
            return new LuaTable { IsInit = false };
        }

        public static LuaTable CreateList(LuaObject[] args) {
            var luaTable = new LuaTable { IsList = true, IsInit = true, list = new LuaList() };
            for (var i = 0; i < args.Length; i ++) {
                luaTable.list.Add(args[i]);
            }
            return luaTable;
        }

        public static LuaTable CreateTable(LuaObject[] args) {
            var luaTable = new LuaTable { IsList = false, IsInit = true, dict = new LuaDictionary() };
            for (var i = 0; i < args.Length; i += 2) {
                luaTable.dict[args[i]] = args[i + 1];
            }
            return luaTable;
        }

    }

}