using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleContext {

		public static readonly string[] emptyList = new string[0];
		public string[] argsList = emptyList;
		public Dictionary<string, ModuleContext> childDict;
		public LVM vm;
		public string name;
		public int level;
		public List<ByteCode> list;
	    public Stack<Queue<LuaLabel>> breakStack = new Stack<Queue<LuaLabel>>();

		public int Count {
			get { return list.Count; }
		}

		public string NewUID() {
			return vm.NewUID();
		}

		public ModuleContext(LVM vm, string name, int level) {
			this.vm = vm;
			this.name = name;
			this.level = level;
			list = new List<ByteCode>();
			childDict = new Dictionary<string, ModuleContext>();
		}

		public ModuleContext Bind(string name, ModuleContext context) {
			return childDict[name] = context;
		}

		public void Add(ByteCode byteCode) {
		    byteCode.index = Count;
            list.Add(byteCode);
        }

        public void BeginLoop() {
            breakStack.Push(new Queue<LuaLabel>());
        }

	    public void OnBreak(LuaLabel label) {
	        breakStack.Peek().Enqueue(label);
	    }

        public void EndLoop(LuaLabel endLabel) {
            var queue = breakStack.Pop();
            while (queue.Count > 0) {
                var item = queue.Dequeue();
                item.value = endLabel.value;
                item.index = endLabel.index;
            }
        }

	}

}