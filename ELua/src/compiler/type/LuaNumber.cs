namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNumber : LuaObject {

		public float value;

	    public override LuaObject Multiply(LuaObject obj) {
            return new LuaNumber { value = value * ((LuaNumber)obj).value };
        }

	    public override LuaObject Division(LuaObject obj) {
            return new LuaNumber { value = value / ((LuaNumber)obj).value };
        }

	    public override LuaObject Mod(LuaObject obj) {
            return new LuaNumber { value = value % ((LuaNumber)obj).value };
        }

        public override LuaObject Plus(LuaObject obj) {
            return new LuaNumber { value = value + ((LuaNumber)obj).value };
        }

        public override LuaObject Subtract(LuaObject obj) {
            return new LuaNumber { value = value - ((LuaNumber)obj).value };
        }

		public override string ToString(StackFrame stackFrame) {
			return value.ToString();
		}

		public override string ToString() {
			return value.ToString();
		}

	}

}