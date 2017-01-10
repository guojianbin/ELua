using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxContext {

		public Parser parser;
		public ulong uid;
	    public int level;
		public List<Expression> list;
		public bool IsCutting;
		public Expression cuttingExp;

		public string NewUID() {
			return string.Format("_i{0}_<{1}>", level.ToString(), (++uid).ToString());
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