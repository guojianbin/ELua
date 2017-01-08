using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public string name;

		public override LuaObject Call(StackFrame stackFrame) {
		    throw new NotImplementedException();	
		}

		public override LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
			throw new NotImplementedException();
		}

		public override string ToString() {
			return string.Format("function {0}", name);
		}

	}

}
