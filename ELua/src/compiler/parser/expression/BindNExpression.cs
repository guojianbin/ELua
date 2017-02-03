using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BindNExpression : Expression {

		public Expression leftList;
		public Expression rightList;

		public BindNExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Bind;
			debugInfo = list[position].debugInfo;
			leftList = list[position];
			rightList = list[position + 2];
		}

	    public BindNExpression(Expression leftList, Expression rightList) {
            isStatement = true;
            type = Type.Bind;
            this.leftList = leftList;
	        this.rightList = rightList;
	    }

	    public override void Extract(SyntaxContext context) {
			leftList.Extract(context);
			rightList.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			rightList.Generate(context);
			leftList.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.BindN, opArg = new LuaInteger(context.vm, ((LeftList2Expression)leftList).itemsList.Count) });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(leftList, rightList);
		}

		public override string ToString() {
			return string.Format("{0} = {1}", leftList, rightList);
		}

	}

}