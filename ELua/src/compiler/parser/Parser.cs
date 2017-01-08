using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Parser {

		public List<Expression> list;

		public Parser(List<Expression> list) {
			this.list = list;
			list.Add(new EOSExpression());

			Parse();
			Extract();
		}

		private void Parse() {
			var context = new SyntaxContext { list = list };
			var parser = new ModuleParser();
			parser.Parse(context, 0);
			list.RemoveAt(list.Count - 1);

			var expList = list.ToArray();
			list.Clear();
			var errorList = new List<Expression>();
			foreach (var item in expList) {
				if (item.IsStatement) {
					list.Add(item);
				} else {
					errorList.Add(item);
				}
			}
		    Console.WriteLine("source:");
			Console.WriteLine(string.Join("\n", list.Select(t => t.ToString())));
			if (errorList.Count > 0) {
				Console.WriteLine();
				Console.WriteLine(string.Join("\n", errorList.Select(t => string.Format("[ERROR -->> {0}] {1}", t.GetDebugInfo(), t))));
			}
		}

		private void Extract() {
			var context = new SyntaxContext { list = new List<Expression>() };
			foreach (var item in list) {
				item.Extract(context);
				context.Add(item);
			}

			list = context.list;
			Console.WriteLine();
		    Console.WriteLine("extract:");
			Console.WriteLine(string.Join("\n", context.list.Select(t => t.ToString())));
		}

		public List<IL> Generate() {
			var context = new ILContext();
			foreach (var item in list) {
				item.Generate(context);
			}

			Console.WriteLine();
		    Console.WriteLine("il:");
			Console.WriteLine(string.Join("\n", context.list.Select(t => t.ToString())));
			return context.list;
		}

	}

}