using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionExpression : Expression {

		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			var itemsList = list.Skip(position + 4).Take(len - 5).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var module = new Module(new ModuleContext(context.vm, nameExp.value, context.level + 1));
			context.vm.Add(module);
			moduleExp.Generate(context.Bind(nameExp.value, module.context));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = new LuaFunction(context.vm, context.vm.NewUID(), module) });
			nameExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", nameExp, moduleExp);
		}

	}

}