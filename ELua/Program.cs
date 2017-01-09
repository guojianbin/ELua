﻿using System;
using System.IO;
using System.Linq;

namespace ELua {

	internal static class Program {

		private static void Main() {
			var file = "test.lua";
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(scanner.Tokens.Select(t => ParserHelper.ToExpression(t)).ToList());
			var module = new Module { name = "main", byteCodes = parser.Generate() };
			var vm = new LVM();
			vm.Add(module);

			Console.WriteLine();
			vm.Run("main");
		}

	}

}