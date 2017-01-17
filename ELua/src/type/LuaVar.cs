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
		    uid = vm.NewUID();
		}

	    public override void Bind(LuaObject obj) {
            target = obj;
            binder.Rebind(obj);
	    }

		public override LuaObject ToObject() {
			return target;
		}

        public override string ToString() {
			return name;
		}

	}

}