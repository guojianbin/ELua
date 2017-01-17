namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaLabel : LuaObject {

		public string value;
		public int index;

		public LuaLabel(LVM vm, string value, int index) : base(vm) {
			this.value = value;
			this.index = index;
			uid = vm.NewUID();
		}

		public override string ToString() {
			return string.Format("{0}->{1}", value, index);
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