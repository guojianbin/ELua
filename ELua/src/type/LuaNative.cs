using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNative : LuaObject {

		public string name;
		public Action<StackFrame, LuaObject[]> func;

		public LuaNative(LVM vm, string name, Action<StackFrame, LuaObject[]> func) : base(vm, Type.Native) {
			this.name = name;
			this.func = func;
			uid = vm.NewUID();
		}

		public override bool ToBoolean() {
			return true;
		}

		public override LuaObject ToObject() {
			return this;
		}

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			func(stackFrame, args);
		}

		public override LuaObject Equal(LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override string ToString() {
            return string.Format("native:{0}", name);
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