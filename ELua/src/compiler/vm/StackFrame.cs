using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackFrame {

		public StackFrame parent;
		public StackContext context;
		public Stack<LuaObject> stack;

		public StackFrame(StackFrame parent) {
			this.parent = parent;
			context = new StackContext();
			stack = new Stack<LuaObject>();
		}

		public void Push(LuaObject obj) {
			stack.Push(obj);
		}

		public LuaObject Pop() {
			return stack.Pop();
		}

		public void Save(LuaVar obj) {
			context.Save(obj.value, stack.Pop());
		}

		public LuaObject[] TakeAll() {
			var arr = stack.ToArray();
			stack.Clear();
			return arr;
		}

		public LuaObject Find(string name) {
			LuaObject value;
			if (context.TryGetValue(name, out value)) {
				return value;
			} else {
				if (parent != null) {
					return parent.Find(name);
				} else {
					return null;
				}
			}
		}

	}

}