using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaTuple : LuaObject {

		public List<LuaObject> list = new List<LuaObject>();

		public LuaTuple(LVM vm, IEnumerable<LuaObject> list) : base(vm) {
			uid = vm.NewUID();
			AddRange(list);
		}

		public override void Unpack(StackFrame stackFrame) {
			for (var i = list.Count - 1; i >= 0; i--) {
				list[i].Unpack(stackFrame);
			}
		}

		public void Add(LuaObject item) {
			list.Add(item);
		}

		public void AddRange(IEnumerable<LuaObject> list) {
			this.list.AddRange(list);
		}

		public void Clear() {
			list.Clear();
		}

		public override LuaObject ToObject() {
			return this;
		}

		public override string ToString() {
			return string.Join(", ", list.Select(t => t.ToString()));
		}

	}

}