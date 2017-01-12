namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject {

		public string name;

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
	        return stackFrame.Find(name).GetProperty(stackFrame, obj);
	    }

	    public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).GetIndex(stackFrame, obj);
        }

	    public override LuaObject Bind(StackFrame stackFrame, LuaObject obj) {
            stackFrame.Bind(name, obj);
	        return this;
	    }

	    public override void Call(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Find(name).Call(stackFrame, args);
		}

	    public override LuaObject Less(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Less(stackFrame, obj);
        }

	    public override LuaObject Greater(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Greater(stackFrame, obj);
        }

	    public override LuaObject LessEqual(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).LessEqual(stackFrame, obj);
        }

	    public override LuaObject GreaterEqual(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).GreaterEqual(stackFrame, obj);
        }

	    public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Equal(stackFrame, obj);
        }

	    public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).NotEqual(stackFrame, obj);
        }

	    public override LuaObject Negate(StackFrame stackFrame) {
            return stackFrame.Find(name).Negate(stackFrame);
        }

	    public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Multiply(stackFrame, obj);
        }

	    public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Division(stackFrame, obj);
        }

	    public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Mod(stackFrame, obj);
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Plus(stackFrame, obj);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Subtract(stackFrame, obj);
        }

	    public override LuaNumber ToNumber(StackFrame stackFrame) {
	        return stackFrame.Find(name).ToNumber(stackFrame);
        }

		public override LuaObject ToObject(StackFrame stackFrame) {
			return stackFrame.Find(name);
		}

		public override bool ToBoolean(StackFrame stackFrame) {
            return stackFrame.Find(name).ToBoolean(stackFrame);
        }

        public override string ToString() {
			return name;
		}

	}

}