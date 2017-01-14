using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionAExpression : Expression {

		public string name;
		public Expression moduleExp;

		public FunctionAExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			var itemsList = list.Skip(position + 3).Take(len - 4).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			name = context.NewUID();
			var module = new Module(new ModuleContext(context.vm, name, context.level + 1));
			context.vm.Add(module);
			moduleExp.Generate(context.Bind(name, module.context));
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg1 = new LuaModule(context.vm, module) });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, moduleExp);
		}

	}

}