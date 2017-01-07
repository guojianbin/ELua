using System;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxContext {

		public ulong uid;
		public List<Expression> list;

		public string NewUID() {
			return String.Format("__<{0}>", (++uid).ToString("D3"));
		}

		public void Add(Expression expression) {
			list.Add(expression);
		}

		public void Insert(int position, Expression expression) {
			list.Insert(position, expression);
		}

		public void Remove(int index, int count) {
			list.RemoveRange(index, count);
		}

	}

}