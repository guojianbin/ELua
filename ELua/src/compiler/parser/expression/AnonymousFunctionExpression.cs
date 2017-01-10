using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class AnonymousFunctionExpression : Expression {

		public string name;
		public ChunkExpression chunkExp;

		public AnonymousFunctionExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			IsSimplify = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			var itemsList = list.Skip(position + 3).Take(len - 4).ToList();
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
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaFunction { name = name } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(chunkExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, chunkExp);
		}

	}

}