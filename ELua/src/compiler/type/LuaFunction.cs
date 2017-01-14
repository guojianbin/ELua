namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public Module module;

		public LuaFunction(LVM vm, Module module) : base(vm) {
			this.module = module;
            uid = vm.NewUID();
        }

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			module.Call(stackFrame, module.context.argsList, args);
		}

		public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override bool ToBoolean(StackFrame stackFrame) {
			return true;
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Format("function {0}", module.name);
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