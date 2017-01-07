using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaUserdata : LuaObject, ICallable {

		public object value;

		public override string ToString() {
			return string.Format("LuaUserdata Value: {0}", value);
		}

		public void Call(StackFrame stackFrame) {
			throw new NotImplementedException();
		}

		public void Call(StackFrame stackFrame, params LuaObject[] args) {
			((Delegate)value).DynamicInvoke(stackFrame, args);
		}

	}

}