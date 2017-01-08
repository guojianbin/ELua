using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackContext {

		public Dictionary<string, LuaObject> dict;

		public StackContext() {
			dict = new Dictionary<string, LuaObject>();
		}

		public void Bind(string name, LuaObject obj) {
			dict[name] = obj;
		}

		public bool TryGetValue(string key, out LuaObject value) {
			return dict.TryGetValue(key, out value);
		}

	}

}