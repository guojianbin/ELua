using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LengthExpression : Expression {

		public Expression targetExp;

		public LengthExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Length;
			debugInfo = list[position].debugInfo;
			targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
		}

		public override void Generate(ModuleContext context) {
			targetExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = context.vm.luaLibrary.lenFunc });
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Call });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return string.Format("#{0}", targetExp);
		}

	}

}