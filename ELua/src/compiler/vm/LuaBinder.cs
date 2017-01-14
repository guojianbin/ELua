namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public struct LuaBinder {

		public StackFrame stackFrame;
		public string name;
		public LuaObject obj;

	    public LuaBinder(StackFrame stackFrame, string name, LuaObject obj) {
	        this.stackFrame = stackFrame;
	        this.name = name;
	        this.obj = obj;
	    }

	    public override string ToString() {
	        return string.Format("{0}:{1}", name, obj);
	    }

	}

}