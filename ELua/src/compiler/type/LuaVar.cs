using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject {

		public string value;

		public override string ToString(StackFrame stackFrame) {
			return stackFrame.Find(value).ToString(stackFrame);
		}

	    public override void Call(StackFrame stackFrame) {
	        throw new NotImplementedException();
	    }

	    public override void Call(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Find(value).Call(stackFrame, args);
		}

		public override string ToString() {
			return value;
		}

	}

}