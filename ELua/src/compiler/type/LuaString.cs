using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaString : LuaObject {

		public string value;

		public override string ToString() {
			return String.Format("\"{0}\"", value);
		}

	}

}