using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaArgs : LuaObject {

		public string[] argsList = ModuleContext.emptyList;

		public override string ToString() {
			return string.Join(", ", argsList.Select(t => t.ToString()));
		}

	}

}