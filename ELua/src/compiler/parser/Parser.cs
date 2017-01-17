using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Parser {

		public LVM vm;
		public Expression ast;
		public List<Expression> errorList;
		public string file;

		public Parser(LVM vm, string file, List<Expression> list) {
			this.vm = vm;
			this.file = file;
			Parse(list);
			Extract();
		}

		private void Parse(List<Expression> list) {
			list.Add(new EOSExpression());
			var context = new SyntaxContext(this, list);
			var parser = new ModuleParser();
			parser.Parse(context, 0);
			list.RemoveAt(list.Count - 1);
			ast = list[0];
			errorList = list.Where(t => !t.IsModule).ToList();

			vm.WriteLine("[source]");
			vm.WriteLine(ast.ToString());
			if (errorList.Count > 0) {
				vm.WriteLine(string.Empty);
				vm.WriteLine(errorList.Select(t => string.Format("[ERROR -->> {0}] {1}", t.GetDebugInfo(), t)).FormatListString("\n"));
			}
		}

		private void Extract() {
			ast.Extract(new SyntaxContext(this, 0));
			vm.WriteLine(string.Empty, Logger.Type.File);
			vm.WriteLine("[extract]", Logger.Type.File);
			vm.WriteLine(ast.ToString(), Logger.Type.File);
		}

		public void Generate() {
			var context = new ModuleContext(vm, file, 0);
            ast.Generate(context);
			vm.Add(new Module(context));
		}

	}

}