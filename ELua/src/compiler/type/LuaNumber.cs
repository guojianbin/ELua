namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNumber : LuaObject {

		public float value;

	    public override LuaObject Negate(StackFrame stackFrame) {
            return new LuaNumber { value = -value };
        }

	    public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return new LuaNumber { value = value * obj.ToNumber(stackFrame).value };
        }

	    public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return new LuaNumber { value = value / obj.ToNumber(stackFrame).value };
        }

	    public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return new LuaNumber { value = value % obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return new LuaNumber { value = value + obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return new LuaNumber { value = value - obj.ToNumber(stackFrame).value };
        }

	    public override LuaNumber ToNumber(StackFrame stackFrame) {
	        return this;
	    }

	    public override string ToString(StackFrame stackFrame) {
			return value.ToString();
		}

		public override string ToString() {
			return value.ToString();
		}

	}

}