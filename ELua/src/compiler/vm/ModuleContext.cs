using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleContext {

        public ulong uid;
        public List<ByteCode> list;
		public string name;
		public Dictionary<string, ModuleContext> funcsDict;

		public int Count {
			get { return list.Count; }
		}

		public string NewUID() {
            return string.Format("_v_<{0}>", (++uid).ToString());
        }

		public string NewLabel() {
            return string.Format("_l_<{0}>", (++uid).ToString());
        }

        public ModuleContext() {
			list = new List<ByteCode>();
			funcsDict = new Dictionary<string, ModuleContext>();
		}

		public void Add(ByteCode byteCode) {
		    byteCode.index = Count;
            list.Add(byteCode);
		}

	}

}