using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionNExpression : Expression {

		public string[] argsList;
		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionNExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			argsList = ((WordListExpression)list[position + 3]).ToParams();
            moduleExp = list[position + 5];
        }

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var module = new Module(new ModuleContext(context.vm, nameExp.value, context.level + 1) { argsList = argsList });
			context.vm.Add(module);
			moduleExp.Generate(module.context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg = new LuaModule(context.vm, module) });
			nameExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}({1})\n{2}\nend", nameExp, argsList.FormatListString(), moduleExp);
		}

	}

}