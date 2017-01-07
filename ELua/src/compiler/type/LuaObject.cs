using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaObject {

		public virtual LuaObject Plus(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

		public virtual string ToString(StackFrame stackFrame) {
			throw new InvalidOperationException(GetType().Name);
		}

	}

}