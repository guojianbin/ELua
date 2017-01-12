namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNil : LuaObject {

		public override void Unpack(StackFrame stackFrame) {
			// ignored
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override bool ToBoolean(StackFrame stackFrame) {
			return false;
		}

		public override string ToString() {
			return "nil";
		}

	}

}