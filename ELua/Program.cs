using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ELua {

	internal static class Program {

		private static void Main() {
			var file = "test.lua";
			var logger = new Logger("log.txt");
			var vm = new LVM(logger);
		    var sw = Stopwatch.StartNew();
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
		    logger.WriteLine(string.Format("complie {0}ms", sw.ElapsedMilliseconds));

			foreach (var module in vm.modulesDict.Values) {
				logger.WriteLine(string.Empty, Logger.Type.File);
				logger.WriteLine(string.Format("[{0}]", module.name), Logger.Type.File);
				logger.WriteLine(string.Join("\n", module.codesList.Select(t => t.ToString())), Logger.Type.File);
			}

			logger.WriteLine(string.Empty);
			logger.WriteLine("[output]");
			vm.Call(file);
            vm.Dispose();
            logger.Flush();
        }

	}

}