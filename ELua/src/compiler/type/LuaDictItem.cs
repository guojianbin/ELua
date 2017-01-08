namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaDictItem : LuaObject {

        public LuaTable table;
        public LuaObject key;
        public LuaObject value;

        public override void Bind(StackFrame stackFrame, LuaObject obj) {
            table.dict[key] = obj;
        }

        public override string ToString(StackFrame stackFrame) {
            return value.ToString(stackFrame);
        }

    }

}