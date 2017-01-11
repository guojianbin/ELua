namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaInteger : LuaObject {

		public int value;

		public override string ToString() {
			return value.ToString();
		}

	}

}