using System.IO;

namespace ELua {

	internal static class Program {

		private static void Main() {
			var file = "test.lua";
			var logger = new Logger("log.txt");
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(logger, file, ParserHelper.ToExpressionList(scanner.Tokens));
			var vm = new LVM(logger);
			vm.Generate(parser.Generate());

			logger.WriteLine(string.Empty);
			logger.WriteLine("<run>");
			vm.Call(file);
		}

	}

}