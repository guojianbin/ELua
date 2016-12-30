using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Call0Parser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsRightValue) {
				return false;
			}
			if (!item2.IsOperator("(")) {
				return false;
			}
			if (!item3.IsOperator(")")) {
				return false;
			} else {
				parser.list.Insert(position, new CallExpression(item1));
				parser.list.RemoveRange(position + 1, 3);
				return true;
			}
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Call1Parser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsRightValue) {
				return false;
			}
			if (!item2.IsOperator("(")) {
				return false;
			}
			parser.Parse(0, parser.parsers.Count, position + 2);
			if (!item3.IsRightValue) {
				return false;
			}
			if (!item4.IsOperator(")")) {
				return false;
			} else {
				parser.list.Insert(position, new Call1Expression(item1, item3));
				parser.list.RemoveRange(position + 1, 4);
				return true;
			}
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Call9Parser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsRightValue) {
				return false;
			}
			if (!item2.IsOperator("(")) {
				return false;
			}
			var index = 2;
			while (true) {
				parser.Parse(0, parser.parsers.Count, position + index);
				if (!item9(index).IsRightValue) {
					return false;
				} else {
					index += 1;
					if (!item9(index).IsOperator(",")) {
						break;
					} else {
						index += 1;
					}
				}
			}
			if (!item9(index).IsOperator(")")) {
				return false;
			} else {
				var begin = position + 2;
				var end = position + index;
				var list = new List<Expression>();
				for (var i = begin; i < end; i += 2) {
					list.Add(parser.list[i]);
				}
				parser.list.Insert(position, new Call9Expression(item1, list));
				parser.list.RemoveRange(position + 1, index + 1);
				return true;
			}
		}

	}

}