using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SyntaxContext {

		public Parser parser;
		public LVM vm;
	    public int level;
		public List<Expression> list;
		public bool isMissed;
		public bool isCutting;
		public Expression cuttingExp;

		public SyntaxContext(Parser parser, int level) {
			this.parser = parser;
			this.level = level;
			vm = parser.vm;
			list = new List<Expression>();
		}

		public SyntaxContext(Parser parser, List<Expression> list) {
			this.parser = parser;
			this.list = list;
			vm = parser.vm;
		}

		public void Add(Expression expression) {
			list.Add(expression);
		}

		public string NewUID() {
			return vm.NewUID();
		}

		public void Insert(int position, Expression expression) {
			list.Insert(position, expression);
		}

		public void Remove(int index, int count) {
			list.RemoveRange(index, count);
		}

	}

}