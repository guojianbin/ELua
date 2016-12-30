using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BaseParser {

		public int level;
		public Parser parser;
		public int position;

		public Expression item1 {
			get { return parser.list[position]; }
		}

		public Expression item2 {
			get { return parser.list[position + 1]; }
		}

		public Expression item3 {
			get { return parser.list[position + 2]; }
		}

		public Expression item4 {
			get { return parser.list[position + 3]; }
		}

		public Expression item9(int offset) {
			return parser.list[position + offset];
		}

		public BaseParser SetLevel(int level) {
			this.level = level;
			return this;
		}

		public virtual bool Parse(Parser parser, int position) {
			throw new InvalidOperationException();
		}

	}

}