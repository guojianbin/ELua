using System.IO;
using System.Linq;

namespace ELua {

	internal static class Program {

		private static void Main() {
			var file = "test.lua";
			var logger = new Logger("log.txt");
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(logger, file, scanner.Tokens.Select(t => ParserHelper.ToExpression(t)).ToList());
			var module = new Module(logger, parser.Generate());
			var vm = new LVM();
			vm.Add(module);

			logger.WriteLine(string.Empty);
			logger.WriteLine("<run>");
			logger.WriteLine(vm.Call(file).ToString());
		}

	}

}