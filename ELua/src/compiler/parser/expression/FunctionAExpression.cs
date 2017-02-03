using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionAExpression : Expression {

		public string name;
		public string[] argsList;
		public Expression moduleExp;

		public FunctionAExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			argsList = ((WordList1Expression)list[position + 2]).ToParams();
			moduleExp = list[position + 4];
        }

		public FunctionAExpression(string name, string[] argsList, Expression moduleExp) {
			isRightValue = true;
			type = Type.Function;
			debugInfo = moduleExp.debugInfo;
			this.name = name;
			this.argsList = argsList;
			this.moduleExp = moduleExp;
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			name = context.NewUID();
			var module = new Module(new ModuleContext(context.vm, name, context.level + 1) { argsList = argsList });
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