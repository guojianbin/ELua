using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public string value;

		public override void Call(StackFrame stackFrame) {
			
		}

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			throw new NotImplementedException();
		}

		public override string ToString() {
			return string.Format("LuaFunction Value: {0}", value);
		}

	}

}
