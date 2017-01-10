using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaUserdata : LuaObject {

		public object value;

	    public override LuaNumber ToNumber(StackFrame stackFrame) {
	        return new LuaNumber { value = Convert.ToSingle(value) };
	    }

	    public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Format("LuaUserdata Value: {0}", value);
		}

		public override LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
		    return ((Func<StackFrame, LuaObject[], LuaObject>)value)(stackFrame, args);
		}

	}

}