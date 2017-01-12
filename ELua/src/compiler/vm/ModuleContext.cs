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

	}

}