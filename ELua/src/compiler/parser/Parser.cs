using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Parser {

		public ModuleExpression module;

		public Parser(List<Expression> list) {
			Parse(list);
			Extract();
		}

		private void Parse(List<Expression> list) {
			list.Add(new EOSExpression());
			var context = new SyntaxContext { list = list };
			var parser = new ModuleParser();
			parser.Parse(context, 0);
			list.RemoveAt(list.Count - 1);

			module = (ModuleExpression)list[0];
			var itemsList = module.itemsList.ToArray();
			var errorList = new List<Expression>();
			module.itemsList.Clear();

			foreach (var item in itemsList) {
				if (item.IsStatement) {
					module.itemsList.Add(item);
				} else {
					errorList.Add(item);
				}
			}
		    Console.WriteLine("source:");
			Console.WriteLine(string.Join("\n", module.itemsList.Select(t => t.ToString())));
			if (errorList.Count > 0) {
				Console.WriteLine();
				Console.WriteLine(string.Join("\n", errorList.Select(t => string.Format("[ERROR -->> {0}] {1}", t.GetDebugInfo(), t))));
			}
		}

		private void Extract() {
            module.Extract(null);
			Console.WriteLine();
		    Console.WriteLine("extract:");
			Console.WriteLine(string.Join("\n", module.itemsList.Select(t => t.ToString())));
		}

		public List<ByteCode> Generate() {
			var context = new ByteCodeContext();
            module.Generate(context);

			Console.WriteLine();
		    Console.WriteLine("il:");
			Console.WriteLine(string.Join("\n", context.list.Select(t => t.ToString())));
			return context.list;
		}

	}

}