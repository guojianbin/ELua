using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionAExpression : Expression {

		public string name;
		public Expression moduleExp;

		public FunctionAExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			var itemsList = list.GetRange(position + 3, len - 4);
			moduleExp = new ModuleExpression(itemsList);
		}

		public FunctionAExpression(string name, Expression moduleExp) {
			isRightValue = true;
			type = Type.Function;
			debugInfo = moduleExp.debugInfo;
			this.name = name;
			this.moduleExp = moduleExp;
		}

		public override void Extract(SyntaxContext context) {
			name = context.NewUID();
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var module = new Module(new ModuleContext(context.vm, name, context.level + 1));
			context.vm.Add(module);
			moduleExp.Generate(module.context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg = new LuaModule(context.vm, module) });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", name, moduleExp);
		}

	}

}