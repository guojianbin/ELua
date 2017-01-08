namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public LuaDictionary dict;
        public LuaList list;
        public bool IsList;
        public bool IsInit;

        public override string ToString(StackFrame stackFrame) {
            if (!IsInit) {
                return "{ }";
            } else if (IsList) {
                return list.ToString(stackFrame);
            } else {
                return dict.ToString(stackFrame);
            }
        }

        public override string ToString() {
            return "table";
        }

    }

}