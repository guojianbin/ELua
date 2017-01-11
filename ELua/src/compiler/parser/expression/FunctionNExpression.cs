using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionNExpression : Expression {

		public string[] argsList;
		public WordExpression nameExp;
		public ChunkExpression chunkExp;

		public FunctionNExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			var wordList = list.Skip(position + 3).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
			var itemsList = list.Skip(position + 3 + argsList.Length * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			chunkExp = new ChunkExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			chunkExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			chunkExp.Generate(context.Bind(nameExp.value, new ModuleContext { name = nameExp.value, level = context.level + 1 }));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg1 = new LuaString { value = nameExp.value }, opArg2 = new LuaArgs { argsList= argsList } });
			nameExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(chunkExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", nameExp, chunkExp);
		}

	}

}