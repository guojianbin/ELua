using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Doge {

    public static class Program2 {

        public static void Main() {
            var syntaxStrList = File.ReadAllLines("syntax.lua");
            var tempStr = File.ReadAllText("template2.txt");
            var tempDict = ParseTemp(tempStr);
            WriteParser(syntaxStrList, tempDict);
        }

        private static void WriteParser(string[] syntaxStrList, Dictionary<string, string> tempDict) {
            var syntaxList = new List<SyntaxInfo>();
            var syntaxDict = new Dictionary<string, SyntaxInfo>();
            var exprDict = new Dictionary<string, SyntaxInfo>();
            var statDict = new Dictionary<string, SyntaxInfo>();
            foreach (var syntaxStr in syntaxStrList.Where(t => !string.IsNullOrEmpty(t))) {
                var syntaxInfo = new SyntaxInfo();
                syntaxInfo.index = syntaxList.Count;
                syntaxInfo.Parse(syntaxStr.Trim());
                syntaxList.Add(syntaxInfo);
                syntaxDict.Add(syntaxInfo.name, syntaxInfo);
                if (syntaxInfo.type == SyntaxInfo.Type.Expr) {
                    exprDict.Add(syntaxInfo.name, syntaxInfo);
                } else if (syntaxInfo.type == SyntaxInfo.Type.Stat) {
                    statDict.Add(syntaxInfo.name, syntaxInfo);
                }
            }

            // stat syntax
            var statSyntax = new SyntaxInfo();
            statSyntax.index = syntaxList.Count;
            statSyntax.Parse(string.Format("stat := {0}", string.Join(" | ", statDict.Values.Select(t => t.name))));
            syntaxList.Add(statSyntax);

            // expr syntax
            var exprSyntax = new SyntaxInfo();
            exprSyntax.index = syntaxList.Count;
            exprSyntax.Parse(string.Format("expr := {0}", string.Join(" | ", exprDict.Values.Select(t => t.name))) + " | name | number | string | boolean | nil");
            syntaxList.Add(exprSyntax);

            // unary expr
            var unaryOp = syntaxDict["unary_op"];
            exprDict.Remove("unary_expr");
            foreach (var op in unaryOp.items) {
                var syntaxInfo = new SyntaxInfo();
                syntaxInfo.index = syntaxList.Count;
                syntaxInfo.Parse(string.Format("unary_expr_{0} := {0} expr", op));
                syntaxList.Add(syntaxInfo);
                exprDict.Add(syntaxInfo.name, syntaxInfo);
            }

            // binary expr
            var binaryOp = syntaxDict["binary_op"];
            exprDict.Remove("binary_expr");
            foreach (var op in binaryOp.items) {
                var syntaxInfo = new SyntaxInfo();
                syntaxInfo.index = syntaxList.Count;
                syntaxInfo.Parse(string.Format("binary_expr_{0} := expr {0} expr", op));
                syntaxList.Add(syntaxInfo);
                exprDict.Add(syntaxInfo.name, syntaxInfo);
            }

            var context = new GenerateContext { sb = new StringBuilder(), tempDict = tempDict };
            foreach (var syntaxInfo in syntaxList) {
                syntaxInfo.Generate(context);
            }
        }

        private static Dictionary<string, string> ParseTemp(string content) {
            var tempReg = new Regex(@"(\w+) \=\= \-\-\[\[\r\n((.|\n)*?)\r\n\]\]");
            var tempDict = new Dictionary<string, string>();
            foreach (Match match in tempReg.Matches(content)) {
                tempDict.Add(match.Groups[1].Value.Trim(), match.Groups[2].Value);
            }
            return tempDict;
        }

        /// <summary>
        /// @author Easily
        /// </summary>
        public class GenerateContext {

            public StringBuilder sb;
            public Dictionary<string, string> tempDict;

        }

        /// <summary>
        /// @author Easily
        /// </summary>
        public class SyntaxInfo {

            public enum Type {

                Undef,
                Stat,
                Expr

            }

            public Type type = Type.Undef;
            public int index;
            public string name;
            public List<SyntaxParser> items = new List<SyntaxParser>();

            public void Parse(string str) {
                var arr = str.Split(new[] { ":=" }, StringSplitOptions.RemoveEmptyEntries);
                name = arr[0].Trim();
                if (name.EndsWith("_stat")) {
                    type = Type.Stat;
                } else if (name.EndsWith("_expr")) {
                    type = Type.Expr;
                }
                foreach (var itemStr in arr[1].Trim().Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)) {
                    var item = new SyntaxParser();
                    item.index = items.Count;
                    item.name = name;
                    item.Parse(itemStr);
                    items.Add(item);
                }
            }

            public void Generate(GenerateContext context) {
                foreach (var item in items) {
                    item.Generate(context);
                }
            }

            public override string ToString() {
                return string.Format("{0} := {1}", name, string.Join(" | ", items));
            }

        }

        /// <summary>
        /// @author Easily
        /// </summary>
        public class SyntaxParser {

            public string name;
            public int index;
            public readonly List<SyntaxItem> items = new List<SyntaxItem>();

            public void Parse(string str) {
                var splitArr = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var itemStr in splitArr.Select(t => t.Trim())) {
                    var item = new SyntaxItem();
                    item.index = items.Count;
                    item.Parse(itemStr);
                    items.Add(item);
                }
            }

            public void Generate(GenerateContext context) {
                var funcTemp = context.tempDict["parser_func"];
                funcTemp = funcTemp.Replace("$name$", string.Format("{0}_{1}", name, index));
                foreach (var item in items) {
                    switch (item.type) {
                        case SyntaxItem.Type.Undef:
                            break;
                        case SyntaxItem.Type.BeginAny:
                            break;
                        case SyntaxItem.Type.EndAny:
                            break;
                        case SyntaxItem.Type.BeginOr:
                            break;
                        case SyntaxItem.Type.EndOr:
                            break;
                        case SyntaxItem.Type.String:
                            break;
                        case SyntaxItem.Type.Parser:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                context.sb.Append(funcTemp);
            }

            public override string ToString() {
                return string.Join(" ", items);
            }

        }

        /// <summary>
        /// @author Easily
        /// </summary>
        public class SyntaxItem {

            public enum Type {

                Undef,
                BeginAny,
                EndAny,
                BeginOr,
                EndOr,
                String,
                Parser

            }

            public int index;
            public Type type = Type.Undef;
            public string value;

            public void Parse(string str) {
                if (str == "[") {
                    type = Type.BeginOr;
                } else if (str == "]") {
                    type = Type.EndOr;
                } else if (str == "{") {
                    type = Type.BeginAny;
                } else if (str == "}") {
                    type = Type.EndAny;
                } else if (str.StartsWith("'")) {
                    type = Type.String;
                    value = str.Substring(1, str.Length - 2);
                } else {
                    type = Type.Parser;
                    value = str;
                }
            }

            public override string ToString() {
                switch (type) {
                    case Type.BeginAny:
                        return "{";
                    case Type.EndAny:
                        return "}";
                    case Type.BeginOr:
                        return "[";
                    case Type.EndOr:
                        return "]";
                    case Type.String:
                        return string.Format("'{0}'", value);
                    case Type.Parser:
                        return value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

    }

}