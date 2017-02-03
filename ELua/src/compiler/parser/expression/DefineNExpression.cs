using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineNExpression : Expression {

		public Expression leftList;
		public Expression rightList;

		public DefineNExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Define;
			debugInfo = list[position].debugInfo;
			leftList = list[position + 1];
			rightList = list[position + 3];
			((WordListExpression)leftList).ToVars();
		}

		public DefineNExpression(List<Expression> items1List, List<Expression> items2List) {
			isStatement = true;
			type = Type.Define;
			debugInfo = items1List[0].debugInfo;
			leftList = new WordListExpression(items1List);
			rightList = new RightList1Expression(items2List);
			((WordListExpression)leftList).ToVars();
		}

		public override void Extract(SyntaxContext context) {
			rightList.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			rightList.Generate(context);
			leftList.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.BindN, opArg = new LuaInteger(context.vm, ((WordListExpression)leftList).itemsList.Count) });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(leftList, rightList);
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", leftList, rightList);
		}

	}

}