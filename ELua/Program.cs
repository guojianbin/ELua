using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace ELua {

	internal static class Program {

		public static LVM vm;
		public static Logger logger;
		public static string file;
		public static Timer timer;

		private static void Main() {
			file = "test.lua";
			logger = new Logger("log.txt");
			vm = new LVM(logger);
		    var sw = Stopwatch.StartNew();
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(vm, file, ParserHelper.ToExpressionList(scanner.Tokens));
			parser.Generate();
		    logger.WriteLine(string.Format("complie {0}ms", sw.ElapsedMilliseconds));

			foreach (var module in vm.modulesList) {
				logger.WriteLine(string.Empty, Logger.Type.File);
				logger.WriteLine(string.Format("[{0}]", module.name), Logger.Type.File);
				logger.WriteLine(module.codesList.Select(t => t.ToString()).FormatListString("\n"), Logger.Type.File);
			}

			OnTimer(null);
//			timer = new Timer(OnTimer, null, 0, Timeout.Infinite);
//			Thread.Sleep(1000000);
        }

		public static void OnTimer(object status) {
			logger.WriteLine(string.Empty);
			logger.WriteLine("[output]");
			vm.Call(file);
			logger.Flush();
//			timer.Change(200, Timeout.Infinite);
			GC.Collect();
		}

	}

}