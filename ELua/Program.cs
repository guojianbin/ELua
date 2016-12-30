using System.IO;
using System.Linq;

namespace ELua {

	internal class Program {

		private static void Main() {
//			foreach (var line in File.ReadAllLines("syntax.txt")) {
//				var temp = line.Split(new[] { ":=" }, StringSplitOptions.None);
//				var key = temp[0].Trim();
//				var value = temp[1].Trim();
//				Console.WriteLine(key);
//				foreach (var item in value.Split('|')) {
//					Console.WriteLine(item);
//				}
//			}
//			var file = "F:/My Documents/SVN/game3_2/Game3/Assets/Resources/GameAssets/Luas/src/game/data/login/LuaLoginNetController.lua.txt";
			var file = "f:/Downloads/test.lua";
			var scanner = new Scanner(file, File.ReadAllText(file));
			var parser = new Parser(scanner.Tokens.Select(t => t.ToExpression()).ToList());
			parser.ToString();
		}

	}

}