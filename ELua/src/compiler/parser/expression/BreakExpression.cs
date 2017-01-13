using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BreakExpression : Expression {

		public BreakExpression(List<Expression> list, int position, int len) {
			IsFinally = true;
			type = Type.Break;
			IsStatement = true;
			debugInfo = list[position].debugInfo;
		}

		public BreakExpression() {
			// ignored
		}

		public override void Generate(ModuleContext context) {
			var jumpEnd = new LuaLabel(context.vm, null, 0);
			context.IsBreak = true;
			context.endLabels.Add(jumpEnd);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg1 = jumpEnd });
		}

	    public override void Extract(SyntaxContext context) {
	        // ignored
	    }

	    public override string ToString() {
			return "break";
		}

	}

}