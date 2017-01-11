namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public static readonly string[] emptyList = new string[0];
		public string[] argsList = emptyList;
		public string name;

		public override LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.module.Call(stackFrame, name, argsList, args);
			return stackFrame.nil;
		}

		public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return new LuaBoolean { value = Equals(obj) };
		}

		public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return new LuaBoolean { value = !Equals(obj) };
		}

		public override bool ToBoolean(StackFrame stackFrame) {
			return true;
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Format("function {0}", name);
		}

		public override int GetHashCode() {
			return (uid != null ? uid.GetHashCode() : 0);
		}

		protected bool Equals(LuaFunction other) {
			return string.Equals(uid, other.uid);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			} else if (ReferenceEquals(this, obj)) {
				return true;
			} else if (obj.GetType() != GetType()) {
				return false;
			} else {
				return Equals((LuaFunction)obj);
			}
		}

	}

}