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
		}

		public override string ToString() {
			return string.Format("{0}:{1}", value, index);
		}

		protected bool Equals(LuaLabel other) {
			return string.Equals(value, other.value) && index == other.index;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			} else if (ReferenceEquals(this, obj)) {
				return true;
			} else if (obj.GetType() != GetType()) {
				return false;
			} else {
				return Equals((LuaLabel)obj);
			}
		}

		public override int GetHashCode() {
			unchecked { return ((value != null ? value.GetHashCode() : 0) * 397) ^ index; }
		}

	}

}