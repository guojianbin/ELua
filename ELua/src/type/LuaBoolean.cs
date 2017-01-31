namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaBoolean : LuaObject {

		public bool value;

		public LuaBoolean(LVM vm, bool value) : base(vm, Type.Boolean) {
			this.value = value;
		}

		public override LuaObject Equal(LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override LuaObject ToObject() {
			return this;
		}

		public override bool ToBoolean() {
			return value;
		}

		public override string ToString() {
			return value.ToString();
		}

		public override int GetHashCode() {
			return value.GetHashCode();
		}

		protected bool Equals(LuaBoolean other) {
			return value.Equals(other.value);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			} else if (ReferenceEquals(this, obj)) {
				return true;
			} else if (obj.GetType() != GetType()) {
				return false;
			} else {
				return Equals((LuaBoolean)obj);
			}
		}

	}

}