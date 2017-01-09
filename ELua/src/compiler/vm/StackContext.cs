using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackContext {

		public Dictionary<string, LuaBinder> dict;

		public StackContext() {
			dict = new Dictionary<string, LuaBinder>();
		}

		public void Bind(LuaBinder obj) {
			dict[obj.name] = obj;
		}

		public bool TryGetValue(string key, out LuaBinder value) {
			return dict.TryGetValue(key, out value);
		}

	}

}