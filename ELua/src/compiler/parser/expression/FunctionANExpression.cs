using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionANExpression : Expression {

		public string name;
		public string[] argsList;
		public Expression moduleExp;

		public FunctionANExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			var wordList = list.Skip(position + 2).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
			var itemsList = list.Skip(position + 2 + argsList.Length * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			name = context.NewUID();
			var module = new Module(new ModuleContext(context.vm, name, context.level + 1) { argsList = argsList });
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