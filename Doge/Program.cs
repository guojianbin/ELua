using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Doge {

	public static class Program {

		public static readonly Regex defReg = new Regex(@"\$(\w*?)\$");
		public const string syntaxRoot = "../../../ELua/src/compiler/parser/syntax";

		public static void Main2() {
			var syntaxStr = File.ReadAllText("syntax.txt");
			Console.WriteLine(syntaxStr);
			var defDict = ParseDef(syntaxStr);
			var syntaxDict = ParseSyntax(syntaxStr);
			var expDict = ParseExps(syntaxStr);
			var tempStr = File.ReadAllText("template.txt");
			var tempDict = ParseTemp(tempStr);

			WriteParser(syntaxDict, defDict, expDict, tempDict);
			WriteCreators(syntaxDict, tempDict);
            // WritePools(syntaxDict, tempDict);
		}

		private static void WriteParser(Dictionary<string, string> syntaxDict, Dictionary<string, string> defDict, Dictionary<string, int> expDict, Dictionary<string, string> tempDict) {
			var syntaxList = new List<SyntaxInfo>();
			foreach (var syntaxObj in syntaxDict) {
				var syntaxInfo = new SyntaxInfo();
				syntaxInfo.name = syntaxObj.Key;
				syntaxInfo.Parse(syntaxObj.Value, defDict, expDict);
				syntaxList.Add(syntaxInfo);
			}
			foreach (var syntaxInfo in syntaxList) {
				var fileStr = syntaxInfo.Generate(tempDict);
				var filePath = string.Format("{0}/{1}Parser.cs", syntaxRoot, syntaxInfo.name);
				File.WriteAllText(filePath, fileStr);
			}
        }

        public static void WriteCreators(Dictionary<string, string> syntaxDict, Dictionary<string, string> tempDict) {
            var funcSb = new StringBuilder();
            foreach (var item in syntaxDict) {
                funcSb.Append(tempDict["creator_func"].Replace("$name$", item.Key));
                funcSb.Append('\n');
                funcSb.Append('\n');
            }
            funcSb.Remove(funcSb.Length - 2, 2);
            var content = tempDict["creator_file"].Replace("$func_list$", funcSb.ToString());
            var filePath = string.Format("{0}/ExpressionCreator.cs", syntaxRoot);
            File.WriteAllText(filePath, content);
        }

        public static void WritePools(Dictionary<string, string> syntaxDict, Dictionary<string, string> tempDict) {
            var poolSb = new StringBuilder();
            var getSb = new StringBuilder();
            var putSb = new StringBuilder();
	        foreach (var item in syntaxDict) {
	            poolSb.Append(tempDict["pool_list"].Replace("$name$", item.Key));
	            poolSb.Append('\n');
	            getSb.Append(tempDict["get_parser"].Replace("$name$", item.Key));
	            getSb.Append('\n');
                getSb.Append('\n');
                putSb.Append(tempDict["put_parser"].Replace("$name$", item.Key));
	            putSb.Append('\n');
                putSb.Append('\n');
            }
	        var content = tempDict["pool_file"].Replace("$pool_list$", poolSb.ToString()).Replace("$get_parser$", getSb.ToString()).Replace("$put_parser$", putSb.ToString());
	        var filePath = string.Format("{0}/ParserPools.cs", syntaxRoot);
            File.WriteAllText(filePath, content);
	    }

		private static Dictionary<string, int> ParseExps(string content) {
			var expReg = new Regex(@"#begin exprexsion((.|\n)*?)#end exprexsion");
			var match = expReg.Match(content);
			var expStr = match.Groups[1].Value.Trim();
			var expList = expStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			var expDict = new Dictionary<string, int>();
			for (var i = 0; i < expList.Length; i++) {
				expDict.Add(expList[i], i);
			}
			return expDict;
		}

		private static Dictionary<string, string> ParseTemp(string content) {
			var tempReg = new Regex(@"(\w+) \=\= \-\-\[\[\r\n((.|\n)*?)\r\n\]\]");
			var tempDict = new Dictionary<string, string>();
			foreach (Match match in tempReg.Matches(content)) {
				tempDict.Add(match.Groups[1].Value.Trim(), match.Groups[2].Value);
			}
			return tempDict;
		}

		private static Dictionary<string, string> ParseDef(string content) {
			var defsReg = new Regex(@"#begin define((.|\n)*?)#end define");
			var match = defsReg.Match(content);
			var defStr = match.Groups[1].Value.Trim();
			var defList = defStr.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var dict = new Dictionary<string, string>();
			foreach (var defItem in defList) {
				var data = defItem.Split(new[] { ":=" }, StringSplitOptions.None);
				dict.Add(data[0].Trim(), data[1].Trim());
			}
			return dict;
		}

		private static Dictionary<string, string> ParseSyntax(string content) {
			var syntaxReg = new Regex(@"#begin syntax((.|\n)*?)#end syntax");
			var match = syntaxReg.Match(content);
			var defStr = match.Groups[1].Value.Trim();
			var defList = defStr.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
			var dict = new Dictionary<string, string>();
			foreach (var defItem in defList) {
				var data = defItem.Split(new[] { ":=" }, StringSplitOptions.None);
				dict.Add(data[0].Trim(), data[1].Trim());
			}
			return dict;
		}

		/// <summary>
		/// @author Easily
		/// </summary>
		public class SyntaxInfo {

			public string name;
			public readonly List<SyntaxItem> items = new List<SyntaxItem>();

			public void Parse(string itemsStr, Dictionary<string, string> defDict, Dictionary<string, int> expDict) {
				foreach (var itemStr in itemsStr.Split(new[] { ".." }, StringSplitOptions.RemoveEmptyEntries)) {
					var item = new SyntaxItem();
					item.Parse(itemStr.Trim(), defDict, expDict);
					items.Add(item);
				}
			}

			public string Generate(Dictionary<string, string> tempDict) {
				var fileTemp = tempDict["parser_file"];
				var srcStr = fileTemp.Replace("$name$", name);
				var sb = new StringBuilder();
				for (var i = 0; i < items.Count; i++) {
					sb.Append(items[i].Generate(tempDict, i));
				}
				sb.Append(tempDict["create_exp"].Replace("$name$", name));
				sb.Append('\n');
				if (items.Last().values.ContainsKey("?")) {
					sb.Append(tempDict["missed_ret"]);
					sb.Append('\n');
				}
				sb.Append(tempDict["ret_true"]);
				srcStr = srcStr.Replace("$body$", sb.ToString());
				return srcStr;
			}

			public override string ToString() {
				var sb = new StringBuilder();
				sb.Append(name);
				sb.Append(":=");
				foreach (var item in items) {
					sb.Append(item);
					sb.Append("..");
				}
				sb.Remove(sb.Length - 2, 2);
				return sb.ToString();
			}

		}

		/// <summary>
		/// @author Easily
		/// </summary>
		public class SyntaxItem {

			public Dictionary<string, string> values = new Dictionary<string, string>();

			public void Parse(string str, Dictionary<string, string> defDict, Dictionary<string, int> expDict) {
				foreach (var s in str.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)) {
					var data = s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
					if (data.Length == 2) {
						var match = defReg.Match(data[1]);
						if (match.Success) {
							values.Add(data[0], data[1].Replace(match.Groups[0].Value, defDict[match.Groups[1].Value]));
						} else {
							var valueStr = data[1].Replace("<eq>", "=");
							if (valueStr.StartsWith("[") && valueStr.EndsWith("]")) {
								var rangeStr = valueStr.Substring(1, valueStr.Length - 2);
								var rangeArr = rangeStr.Split(',');
								var begin = expDict[rangeArr[0]];
								var end = expDict[rangeArr[1]];
								var sb = new StringBuilder();
								foreach (var t in expDict.Where(t => t.Value >= begin && t.Value <= end)) {
									sb.Append(t.Key);
									sb.Append(',');
								}
								sb.Remove(sb.Length - 1, 1);
								values.Add(data[0], sb.ToString());
							} else {
								values.Add(data[0], valueStr);
							}
						}
					} else if (data.Length == 1) {
						values.Add(data[0], String.Empty);
					} else {
						throw new Exception(s);
					}
				}
			}

			public string Generate(Dictionary<string, string> tempDict, int index) {
				var contentSb = new StringBuilder();
				if (values.ContainsKey("exclude_exp")) {
					var exTemp = tempDict["exclude_exp"];
					var exStr = exTemp.Replace("$exp$", values["exclude_exp"]);
					contentSb.Append(exStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("exclude_op")) {
					var opTemp = tempDict["exclude_op"];
					var opStr = opTemp.Replace("$op$", values["exclude_op"]);
					contentSb.Append(opStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("+")) {
					contentSb.Append(tempDict["begin_loop2"]);
					contentSb.Append('\n');
				}
                if (values.ContainsKey("<")) {
					contentSb.Append(tempDict["begin_loop"]);
					contentSb.Append('\n');
				}
                if (values.ContainsKey("*")) {
					contentSb.Append(tempDict["begin_loop"]);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("parser")) {
					var parserTemp = tempDict["parser"];
					foreach (var parserStr in values["parser"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
						var itemStr = parserTemp.Replace("$name$", parserStr);
						itemStr = itemStr.Replace("$index$", index.ToString());
						contentSb.Append(itemStr);
						contentSb.Append('\n');
					}
				}
				if (values.ContainsKey("*")) {
					contentSb.Append(tempDict["missed_break"]);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("+")) {
					contentSb.Append(tempDict["missed_break2"]);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("op")) {
					var opTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_op"]);
					var opStr = opTemp.Replace("$index$", index.ToString());
					opStr = opStr.Replace("$op$", values["op"]);
					contentSb.Append(opStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("keyword")) {
					var keywordTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_keyword"]);
					var keywordStr = keywordTemp.Replace("$index$", index.ToString());
					keywordStr = keywordStr.Replace("$keyword$", values["keyword"]);
					contentSb.Append(keywordStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("exp")) {
					var expTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_exp"]);
					var expStr = expTemp.Replace("$index$", index.ToString());
					expStr = expStr.Replace("$exp$", values["exp"]);
					contentSb.Append(expStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("is_left")) {
					var rightTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_left"]);
					var rightStr = rightTemp.Replace("$index$", index.ToString());
					contentSb.Append(rightStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("is_right")) {
					var rightTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_right"]);
					var rightStr = rightTemp.Replace("$index$", index.ToString());
					contentSb.Append(rightStr);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("is_stat")) {
				    var rightTemp = tempDict["if_else"].Replace("$cond$", tempDict["is_stat"]);
		            var rightStr = rightTemp.Replace("$index$", index.ToString());
				    contentSb.Append(rightStr);
				    contentSb.Append('\n');
				}
				contentSb.Append(tempDict["move_next"]);
				contentSb.Append('\n');
				if (values.ContainsKey("?")) {
					contentSb.Replace("$if_exp$", tempDict["is_missed"]);
					contentSb.Append(tempDict["missed_back"]);
					contentSb.Append('\n');
				}
                if (values.ContainsKey("*")) {
					contentSb.Replace("$if_exp$", tempDict["break"]);
                    contentSb.Append(tempDict["end_loop"]);
					contentSb.Append('\n');
				}
				if (values.ContainsKey("+")) {
					contentSb.Replace("$if_exp$", tempDict["break"]);
                    contentSb.Append(tempDict["end_loop2"]);
					contentSb.Append('\n');
                }
				if (values.ContainsKey(">")) {
					contentSb.Append(tempDict["end_loop"]);
					contentSb.Append('\n');
				}

				contentSb.Replace("$if_exp$", tempDict["ret_false"]);
				contentSb.Replace("$else_exp$", tempDict["ignored"]);
				return contentSb.ToString();
			}

			public override string ToString() {
				var sb = new StringBuilder();
				foreach (var item in values) {
					sb.Append(item.Key);
					sb.Append('=');
					sb.Append(item.Value);
					sb.Append(';');
				}
				sb.Remove(sb.Length - 1, 1);
				return sb.ToString();
			}

		}

	}

}