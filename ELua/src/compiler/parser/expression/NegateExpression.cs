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
			_targetExp = Extract(context, _targetExp);
		}

	    public override void Generate(ILContext context) {
            context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaNumber { value = -float.Parse(_targetExp.value) } });
        }

	    public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp);
		}

		public override string ToString() {
			return string.Format("-{0}", _targetExp);
		}

	}

}