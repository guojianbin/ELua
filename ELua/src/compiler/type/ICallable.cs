namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface ICallable {

		void Call(StackFrame stackFrame);
		void Call(StackFrame stackFrame, params LuaObject[] args);

	}

}