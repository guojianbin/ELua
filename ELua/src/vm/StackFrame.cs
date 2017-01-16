using System.Collections;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StackFrame {

		public static readonly LuaObject[] emptyList = new LuaObject[0];
		public string uid;
		public Executor executor;
		public LVM vm;
		public Module module;
		public List<ByteCode> codesList;
		public StackFrame parent;
		public StackFrame upvalue;
		public Dictionary<string, LuaBinder> context;
		public Stack<LuaObject> stack;
		public int level;
		public int position;
		public IEnumerator iterator;

	    public int stackLen {
	        get { return stack.Count; }
	    }

	    public StackFrame(LVM vm) {
			this.vm = vm;
			uid = vm.NewUID();
            context = new Dictionary<string, LuaBinder>();
            stack = new Stack<LuaObject>();
        }

		public StackFrame(Module module, StackFrame parent) {
			this.parent = parent;
			this.module = module;
			vm = parent.vm;
		    uid = vm.NewUID();
		    codesList = module.codesList;
            context = new Dictionary<string, LuaBinder>();
			stack = new Stack<LuaObject>();
			level = parent.level + 1;
			executor = parent.executor;
			iterator = Execute();
	    }

		public StackFrame(Module module, StackFrame parent, StackFrame upvalue) {
			this.parent = parent;
			this.upvalue = upvalue;
			this.module = module;
			vm = parent.vm;
			uid = vm.NewUID();
			codesList = module.codesList;
			context = new Dictionary<string, LuaBinder>();
			stack = new Stack<LuaObject>();
			level = parent.level + 1;
			executor = parent.executor;
			iterator = Execute();
		}

		public void Push(LuaObject obj) {
			stack.Push(obj);
		}

		public LuaObject PopVar() {
			if (stackLen == 0) {
				return vm.nil;
			} else {
				return stack.Pop();
			}
		}

		public LuaObject Pop() {
			if (stackLen == 0) {
				return vm.nil;
			} else {
				return stack.Pop().ToObject();
			}
		}

	    public void Clear() {
	        stack.ClearAll();
	    }

		public LuaObject[] Take(int len) {
			if (len == 0) {
				return emptyList;
			} else {
				var list = new LuaObject[len];
				for (var i = 0; i < len; i++) {
					list[i] = Pop();
				}
				return list;
			}
		}

		public LuaObject[] TakeVars(int len) {
			if (len == 0) {
				return emptyList;
			} else {
				var list = new LuaObject[len];
				for (var i = 0; i < len; i++) {
					list[i] = PopVar();
				}
				return list;
			}
		}

	    public LuaBinder Define(string name) {
	        return Bind(name, vm.nil);
	    }

		public LuaBinder Bind(string name, LuaObject obj) {
			return Bind(new LuaBinder(this, name, obj));
		}

		public LuaBinder Bind(LuaBinder binder) {
			return context[binder.name] = binder;
		}

		public LuaBinder Find(string name) {
			LuaBinder value;
			if (context.TryGetValue(name, out value)) {
				return value;
			}
			value = FindUpvalue(name);
			if (value != null) {
				return Bind(value);
			}
			value = FindParent(name);
			if (value != null) {
				return Bind(value);
			} else {
				return Define(name);
			}
		}

		public LuaBinder FindUpvalue(string name) {
			return upvalue == null ? null : upvalue.Find(name);
		}

		public LuaBinder FindParent(string name) {
			return parent == null ? null : parent.Find(name);
		}

		public void Jump(LuaLabel label) {
			position = label.index;
		}

		public void Return() {
			position = codesList.Count;
		}

		public bool MoveNext() {
			if (!iterator.MoveNext()) {
				return false;
			} else {
				return true;
			}
		}

		public IEnumerator Execute() {
			for (position = 0; position < codesList.Count; position++) {
				codesList[position].Execute(this);
				yield return null;
			}
            parent.Push(PopResult());
		}

	    public LuaObject PopResult() {
            var len = stackLen;
            if (len == 0) {
                return vm.nil;
            } else if (len == 1) {
                return Pop();
            } else {
                return vm.GetTuple(Take(len));
            }
        }

		public override string ToString() {
			return string.Format("{0}:{1}", module.name, level);
		}

	}

}