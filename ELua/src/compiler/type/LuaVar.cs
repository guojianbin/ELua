namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject, ICallable {

		public string value;

		public override string ToString(StackFrame stackFrame) {
			return stackFrame.Find(value).ToString(stackFrame);
		}

		public void Call(StackFrame stackFrame) {
			throw new System.NotImplementedException();
		}

		public void Call(StackFrame stackFrame, params LuaObject[] args) {
			((ICallable)stackFrame.Find(value)).Call(stackFrame, args);
		}

		public override string ToString() {
			return value;
		}

	}

}