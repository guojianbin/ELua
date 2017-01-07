using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaUserdata : LuaObject {

		public object value;

		public override string ToString() {
			return string.Format("LuaUserdata Value: {0}", value);
		}

		public override void Call(StackFrame stackFrame) {
			throw new NotImplementedException();
		}

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
		    ((Action<StackFrame, LuaObject[]>)value)(stackFrame, args);
		}

	}

}