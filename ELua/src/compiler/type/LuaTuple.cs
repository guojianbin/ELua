using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaTuple : LuaObject {

		public LuaObject[] list;

		public override string ToString() {
			return string.Join(", ", list.Select(t => t.ToString()));
		}

	}

}