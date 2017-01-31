namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

		public Module module;
		public StackFrame stackFrame;

		public LuaFunction(LVM vm, Module module, StackFrame stackFrame) : base(vm, Type.Function) {
			this.module = module;
			this.stackFrame = stackFrame;
			uid = vm.NewUID();
        }

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			module.Call(stackFrame, this.stackFrame, module.context.argsList, args);
		}

		public override LuaObject Equal(LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override bool ToBoolean() {
			return true;
		}

		public override LuaObject ToObject() {
			return this;
		}

		public override string ToString() {
			return string.Format("function:{0}", module.name);
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