namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public struct LuaBinder {

		public LVM vm;
		public StackFrame stackFrame;
		public string name;
		public LuaObject obj;

	    public override string ToString() {
	        return string.Format("{0}:{1}", name, obj);
	    }

	}

}