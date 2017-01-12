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
			var module = new Module(new ModuleContext(context.vm, nameExp.value, context.level + 1) { argsList = argsList });
			context.vm.Add(module);
			chunkExp.Generate(context.Bind(nameExp.value, module.context));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = new LuaFunction(module) });
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