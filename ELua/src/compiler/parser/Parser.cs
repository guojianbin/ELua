using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Parser {

		public Logger logger;
		public ModuleExpression module;
		public string file;

		public Parser(Logger logger, string file, List<Expression> list) {
			this.logger = logger;
			this.file = file;
			Parse(list);
			Extract();
		}

		private void Parse(List<Expression> list) {
			list.Add(new EOSExpression());
			var context = new SyntaxContext { list = list, parser = this };
			var parser = new ModuleParser();
			parser.Parse(context, 0);
			list.RemoveAt(list.Count - 1);
			module = (ModuleExpression)list[0];
			var errList = list.Where(t => !t.IsChunked).ToArray();

			logger.WriteLine("<source>");
			logger.WriteLine(module.ToString());
			if (errList.Length > 0) {
				logger.WriteLine(string.Empty);
				logger.WriteLine(string.Join("\n", errList.Select(t => string.Format("[ERROR -->> {0}] {1}", t.GetDebugInfo(), t))));
			}
		}

		private void Extract() {
			module.Extract(new SyntaxContext { parser = this });
			logger.WriteLine(string.Empty);
			logger.WriteLine("<extract>");
			logger.WriteLine(module.ToString());
		}

		public ModuleContext Generate() {
			var context = new ModuleContext { name = file };
            module.Generate(context);
			return context;
		}

	}

}