using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaTuple : LuaObject {

		public LuaObject[] list;

		public LuaTuple(LuaObject[] list) {
			this.list = list;
		}

		public override void Unpack(StackFrame stackFrame) {
			for (var i = list.Length - 1; i >= 0; i--) {
				list[i].Unpack(stackFrame);
			}
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Join(", ", list.Select(t => t.ToString()));
		}

	}

}