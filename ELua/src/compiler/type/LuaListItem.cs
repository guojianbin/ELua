namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaListItem : LuaObject {

        public LuaTable table;
        public LuaList list;
        public int index;
        public LuaObject value;

        public override void Bind(StackFrame stackFrame, LuaObject obj) {
            list[index] = obj;
        }

        public override LuaObject Negate(StackFrame stackFrame) {
            return value.Negate(stackFrame);
        }

        public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return value.Multiply(stackFrame, obj);
        }

        public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return value.Division(stackFrame, obj);
        }

        public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return value.Mod(stackFrame, obj);
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return value.Plus(stackFrame, obj);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return value.Subtract(stackFrame, obj);
        }

        public override LuaNumber ToNumber(StackFrame stackFrame) {
            return value.ToNumber(stackFrame);
        }

        public override string ToString(StackFrame stackFrame) {
            return value.ToString(stackFrame);
        }

    }

}