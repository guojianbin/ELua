namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaBinder {

		public StackFrame stackFrame;
		public string name;
		public LuaObject target;

	    public LuaBinder(StackFrame stackFrame, string name, LuaObject target) {
	        this.stackFrame = stackFrame;
	        this.name = name;
	        this.target = target;
	    }

		public void Rebind(LuaObject target) {
			this.target = target;
		}

	    public override string ToString() {
	        return string.Format("{0}:{1}", name, target);
	    }

	}

}