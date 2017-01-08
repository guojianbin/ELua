using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NegateExpression : Expression {

		private Expression _targetExp;

		public NegateExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Negate;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = ParserHelper.Extract(context, _targetExp);
		}

	    public override void Generate(ILContext context) {
            _targetExp.Generate(context);
            context.Add(new IL { opCode = IL.OpCode.Negate });
        }

	    public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp);
		}

		public override string ToString() {
			return string.Format("-{0}", _targetExp);
		}

	}

}