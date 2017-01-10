using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionExpression : Expression {

		public string name;
		public ChunkExpression chunkExp;

		public FunctionExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			IsSimplify = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			name = ((WordExpression)list[position + 1]).value;
			var itemsList = list.Skip(position + 4).Take(len - 5).ToList();
			chunkExp = new ChunkExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			chunkExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var funcContext = new ModuleContext { name = name };
			context.funcsDict.Add(funcContext.name, funcContext);
			chunkExp.Generate(funcContext);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaFunction { name = name } });
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaVar { name = name } });
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(chunkExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, chunkExp);
		}

	}

}