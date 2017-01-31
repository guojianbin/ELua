namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaInteger : LuaObject {

		public int value;

		public LuaInteger(LVM vm, int value) : base(vm, Type.Integer) {
			this.value = value;
		}

		public override string ToString() {
			return value.ToString();
		}

	}

}