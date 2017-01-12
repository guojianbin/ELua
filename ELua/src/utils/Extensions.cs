using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static void ClearAll<T>(this Stack<T> stack) {
			while (stack.Count > 0) {
				stack.Pop();
			}
		}

	}

}