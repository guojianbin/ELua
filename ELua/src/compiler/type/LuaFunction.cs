using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject, ICallable {

		public string value;

		public void Call(StackFrame stackFrame) {
			
		}

		public void Call(StackFrame stackFrame, params LuaObject[] args) {
			throw new NotImplementedException();
		}

		public override string ToString() {
			return string.Format("LuaFunction Value: {0}", value);
		}

	}

}
