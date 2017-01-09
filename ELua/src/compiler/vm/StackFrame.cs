using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackFrame {

		public static LuaObject[] emptyList = new LuaObject[0];

	    public Module module;
		public StackFrame parent;
		public LuaObject nil;
		public LVM vm;

		public StackContext context;
		public Stack<LuaObject> stack;

	    public int stackLen {
	        get { return stack.Count; }
	    }

	    public StackFrame(LVM vm) {
	        this.vm = vm;
		    nil = vm.nil;
            context = new StackContext();
            stack = new Stack<LuaObject>();
        }

	    public StackFrame(StackFrame parent) {
			this.parent = parent;
	        vm = parent.vm;
			nil = vm.nil;
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

		public LuaObject[] TakeAll() {
			if (stackLen == 0) {
				return emptyList;
			} else {
				var arr = stack.ToArray();
				stack.Clear();
				return arr;
			}
		}

		public void Bind(string name, LuaObject obj) {
			var binder = new LuaBinder { stackFrame = this, name = name, vm = vm, obj = obj };
			context.Bind(binder);
		}

		public void Bind(LuaBinder binder) {
			context.Bind(binder);
		}

		public LuaObject Find(string name) {
			LuaBinder value;
			if (context.TryGetValue(name, out value)) {
				return value.obj;
			} else {
				var obj = FindParent(name);
				Bind(name, obj);
				return obj;
			}
		}

		public LuaObject FindParent(string name) {
			if (parent != null) {
				return parent.Find(name);
			} else {
				return nil;
			}
		}

	}

}