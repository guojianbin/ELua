using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public string name;

		public override void Call(StackFrame stackFrame) {
			
		}

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			throw new NotImplementedException();
		}

		public override string ToString() {
			return string.Format("function {0}", name);
		}

	}

}
