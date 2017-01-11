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
			chunkExp.Generate(context.Bind(name, new ModuleContext { name = name, level = context.level + 1 }));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg1 = new LuaString { value = name }, opArg2 = new LuaArgs { argsList = argsList } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(chunkExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, chunkExp);
		}

	}

}