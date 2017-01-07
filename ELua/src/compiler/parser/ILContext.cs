using System;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ILContext {

		public ulong uid;
		public List<IL> list;

		public ILContext() {
			list = new List<IL>();
		}

		public string NewUID() {
			return String.Format("__<{0}>", (++uid).ToString("D3"));
		}

		public void Add(IL il) {
			list.Add(il);
		}

		public void Insert(int position, IL il) {
			list.Insert(position, il);
		}

		public void Remove(int index, int count) {
			list.RemoveRange(index, count);
		}

	}

}