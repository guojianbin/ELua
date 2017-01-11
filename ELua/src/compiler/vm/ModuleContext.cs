using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleContext {

		public string name;
		public int level;
        public ulong uid;
        public List<ByteCode> list;
		public Dictionary<string, ModuleContext> childDict;

		public int Count {
			get { return list.Count; }
		}

		public ModuleContext() {
			list = new List<ByteCode>();
			childDict = new Dictionary<string, ModuleContext>();
		}

		public string NewUID() {
            return string.Format("_v_i{0}<{1}>", level, (++uid).ToString());
        }

		public string NewLabel() {
            return string.Format("_l_i{0}<{1}>", level, (++uid).ToString());
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