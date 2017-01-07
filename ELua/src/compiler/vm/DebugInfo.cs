using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DebugInfo {

		public string file;
		public int line;
		public int position;

		public static string ToString(Expression expression) {
			return ToString(expression.debugInfo);
		}

		public static string ToString(DebugInfo debugInfo) {
			return string.Format("Line: {0}", debugInfo.line);
		}

		public static string ToString(params Expression[] list) {
			return ToString(list.Select(t => t.debugInfo).ToArray());
		}

		public static string ToString(params DebugInfo[] list) {
			var hashList = new HashSet<int>();
			foreach (var item in list) {
				hashList.Add(item.line);
			}
			return string.Format("Line: {0}", string.Join(", ", hashList.Select(t => t.ToString())));
		}

		public override string ToString() {
			return string.Format("File: {0}, Line: {1}, Position: {2}", file, line, position);
		}

	}

}