namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaBoolean : LuaObject {

        public bool value;

	    public override LuaObject ToObject(StackFrame stackFrame) {
		    return this;
	    }

	    public override bool ToBoolean(StackFrame stackFrame) {
            return value;
        }

        public override string ToString(StackFrame stackFrame) {
            return value.ToString();
        }

        public override string ToString() {
            return value.ToString();
        }

    }

}