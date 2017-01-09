using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ByteCodeContext {

        public ulong uid;
        public List<ByteCode> list;

        public string NewUID() {
            return string.Format("_bc_<{0}>", (++uid).ToString());
        }

        public ByteCodeContext() {
			list = new List<ByteCode>();
		}

		public void Add(ByteCode byteCode) {
		    byteCode.index = list.Count;
            list.Add(byteCode);
		}

	}

}