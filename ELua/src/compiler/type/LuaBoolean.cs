namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaBoolean : LuaObject {

        public bool value;

        public override string ToString(StackFrame stackFrame) {
            return value.ToString();
        }

        public override string ToString() {
            return value.ToString();
        }

    }

}