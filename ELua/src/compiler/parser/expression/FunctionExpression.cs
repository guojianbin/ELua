using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionExpression : Expression {

		public WordExpression nameExp;
		public ChunkExpression chunkExp;

		public FunctionExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			var itemsList = list.Skip(position + 4).Take(len - 5).ToList();
			chunkExp = new ChunkExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			chunkExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			chunkExp.Generate(context.Bind(nameExp.value, new ModuleContext { name = nameExp.value, level = context.level + 1 }));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg1 = new LuaString { value = nameExp.value }, opArg2 = new LuaArgs() });
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