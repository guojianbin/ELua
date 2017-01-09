using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxContext {

		public ulong uid;
	    public int level;
		public List<Expression> list;

		public string NewUID() {
			return string.Format("_var_<{0}>", (++uid).ToString());
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