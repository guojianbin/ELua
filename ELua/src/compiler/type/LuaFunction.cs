namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public string name;
		public string[] argsList;

		public override LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
			return stackFrame.module.Call(stackFrame, name, argsList, args);
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Format("function {0}", name);
		}

	}

}
