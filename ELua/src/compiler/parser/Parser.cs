using System;
using System.Collections.Generic;
using System.Text;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Parser {

		public readonly List<List<Func<BaseParser>>> parsers = new List<List<Func<BaseParser>>>();
		public readonly List<Func<BaseParser>> parsers2 = new List<Func<BaseParser>>();
		public readonly List<Expression> list;

		public Parser(List<Expression> list) {
			this.list = list;
			list.Add(new EOSExpression());

			parsers2.Add(() => new Call0Parser());
			parsers2.Add(() => new Call9Parser());
			parsers2.Add(() => new Call1Parser());
			parsers2.Add(() => new BindParser());
			parsers2.Add(() => new DefineParser());

			parsers.Add(new List<Func<BaseParser>> { Creator(() => new ParenParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new PropertyParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new Call0Parser()), Creator(() => new Call1Parser()), Creator(() => new Call9Parser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new NegateParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new MultiplyParser()), Creator(() => new DivisionParser()), Creator(() => new ModParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new AddParser()), Creator(() => new SubtractParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new BindParser()) });
			parsers.Add(new List<Func<BaseParser>> { Creator(() => new DefineParser()) });

			Parse();
			Check();
		}

		private Func<BaseParser> Creator(Func<BaseParser> func) {
			var level = parsers.Count;
			return () => func().SetLevel(level);
		}

		private void Check() {
			var sb = new StringBuilder();
			foreach (var expression in list) {
				if (expression.IsStatement) {
					sb.Append(expression);
				} else {
					sb.AppendFormat("[ERROR -->> {0}] ", expression.DebugInfo());
					sb.Append(expression);
				}
				sb.Append('\n');
			}
			Console.WriteLine(sb.ToString());
		}

		private void Parse() {
			var position = 0;
			while (true) {
				for (var i = 0; i < parsers.Count; i++) {
					Parse(position, i);
				}
				position += 1;
				if (position >= list.Count) {
					break;
				}
			}
		}

		private void Parse2() {
			var position = 0;
			while (true) {
                Parse(new Call0Parser(), position);
                Parse(new Call1Parser(), position);
                Parse(new Call9Parser(), position);
                Parse(new BindParser(), position);
                Parse(new DefineParser(), position);
				position += 1;
				if (position >= list.Count) {
					break;
				}
			}
		}

	    public void Parse(BaseParser parser, int position) {
	        while (true) {
	            if (!parser.Parse(this, position)) {
	                break;
	            }
	        }
	    }

		public void Parse(int begin, int end, int position) {
			for (var i = begin; i < end; i++) {
				Parse(position, i);
			}
		}

		private void Parse(int position, int i) {
			var list = parsers[i];
			for (var j = 0; j < list.Count; j++) {
				while (true) {
					var parser = list[j]();
					if (!parser.Parse(this, position)) {
						break;
					}
				}
			}
		}

	}

}