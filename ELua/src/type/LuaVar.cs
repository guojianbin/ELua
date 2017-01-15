namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject {

		public string name;
		public LuaBinder binder;
		public LuaObject target;

		public LuaVar(LVM vm, string name, LuaBinder binder) : base(vm) {
			this.name = name;
			this.binder = binder;
			target = binder.target;
		}

	    public override LuaObject Bind(StackFrame stackFrame, LuaObject obj) {
			binder.Rebind(obj);
	        return this;
	    }

		public override LuaObject ToObject(StackFrame stackFrame) {
			return target;
		}

        public override string ToString() {
			return name;
		}

	}

}