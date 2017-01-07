using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineExpression : Expression {

		private Expression _leftExp;
		private Expression _rightExp;

		public DefineExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Define;
			debugInfo = list[position].debugInfo;
			_leftExp = list[position + 1];
			_rightExp = list[position + 3];
		}

		public DefineExpression(Expression leftExp, Expression rightExp) {
			_leftExp = leftExp;
			_rightExp = rightExp;
			type = Type.Define;
			IsStatement = true;
		}

		public override void Generate(ILContext context) {
			_rightExp.Generate(context);
			context.Add(new IL { opCode = IL.OpCode.Save, opArg = new LuaVar { value = _leftExp.value } });
		}

		public override void Extract(SyntaxContext context) {
			_leftExp = Extract(context, _leftExp);
			_rightExp = Extract(context, _rightExp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_leftExp, _rightExp);
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", _leftExp, _rightExp);
		}

	}

}