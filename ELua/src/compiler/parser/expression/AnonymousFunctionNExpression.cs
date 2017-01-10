using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class AnonymousFunctionNExpression : Expression {

		public string name;
		public string[] argsList;
		public ChunkExpression chunkExp;

		public AnonymousFunctionNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			IsSimplify = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			var wordList = list.Skip(position + 2).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
			var itemsList = list.Skip(position + 2 + argsList.Length * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			chunkExp = new ChunkExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			chunkExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			name = context.NewUID();
			var funcContext = new ModuleContext { name = name };
			context.funcsDict.Add(funcContext.name, funcContext);
			chunkExp.Generate(funcContext);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaFunction { name = name, argsList = argsList.ToArray() } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(chunkExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, chunkExp);
		}

	}

}