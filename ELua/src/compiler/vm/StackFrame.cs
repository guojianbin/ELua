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
		public LuaObject nil;
		public LVM vm;
		public Module module;
		public List<ByteCode> codesList;
		public StackFrame parent;
		public Dictionary<string, LuaBinder> context;
		public Stack<LuaObject> stack;
		public int level;
		public int position;
		public IEnumerator iterator;
		public LuaObject result;

	    public int stackLen {
	        get { return stack.Count; }
	    }

	    public StackFrame(LVM vm) {
	        this.vm = vm;
		    nil = vm.nil;
            context = new Dictionary<string, LuaBinder>();
            stack = new Stack<LuaObject>();
        }

		public StackFrame(StackFrame parent, Module module, string uid) {
			this.parent = parent;
		    this.module = module;
		    this.uid = uid;
			vm = parent.vm;
			nil = vm.nil;
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

		public LuaObject PopRaw() {
			return stack.Pop();
		}

		public LuaObject Pop() {
			return PopRaw().ToObject(this);
		}

	    public void Clear() {
	        stack.Clear();
	    }

		public LuaObject[] Take(int len) {
			if (len == 0) {
				return emptyList;
			} else {
				var arr = new LuaObject[len];
				for (var i = 0; i < len; i++) {
					arr[i] = Pop();
				}
				return arr;
			}
		}

		public void Bind(string name, LuaObject obj) {
			context[name] = new LuaBinder { stackFrame = this, name = name, obj = obj };
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

		public void Jump(LuaLabel label) {
			position = label.index;
		}

		public void Return() {
			position = codesList.Count;
		}

		public bool MoveNext() {
			if (!iterator.MoveNext()) {
				parent.Push(result);
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
			if (stackLen == 0) {
				result = nil;
			} else {
				var topObj = PopRaw();
				if (topObj.IsNil) {
					result = nil;
				} else {
					result = topObj.ToObject(this);
				}
			}
		}

		public override string ToString() {
			return string.Format("{0}:{1}", module.name, level);
		}

	}

}