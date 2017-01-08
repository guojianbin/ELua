using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackFrame {

	    public LVM vm;
		public StackFrame parent;
		public StackContext context;
		public Stack<LuaObject> stack;

	    public int stackLen {
	        get { return stack.Count; }
	    }

	    public StackFrame(LVM vm) {
	        this.vm = vm;
            context = new StackContext();
            stack = new Stack<LuaObject>();
        }

	    public StackFrame(StackFrame parent) {
			this.parent = parent;
	        vm = parent.vm;
            context = new StackContext();
			stack = new Stack<LuaObject>();
		}

		public void Push(LuaObject obj) {
			stack.Push(obj);
		}

		public LuaObject Pop() {
			return stack.Pop();
		}

	    public void Clear() {
	        stack.Clear();
	    }

		public void Bind(string name, LuaObject obj) {
			context.Bind(name, obj);
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