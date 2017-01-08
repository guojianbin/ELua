using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineExpression : Expression {

		private Expression _item1Exp;
		private Expression _item2Exp;

		public DefineExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Define;
			debugInfo = list[position].debugInfo;
			_item1Exp = list[position + 1];
			_item2Exp = list[position + 3];
		}

		public DefineExpression(Expression item1Exp, Expression item2Exp) {
			_item1Exp = item1Exp;
			_item2Exp = item2Exp;
			type = Type.Define;
			IsStatement = true;
		}

		public override void Generate(ILContext context) {
			_item2Exp.Generate(context);
			context.Add(new IL { opCode = IL.OpCode.Save, opArg = new LuaVar { value = _item1Exp.GetName() } });
		}

		public override void Extract(SyntaxContext context) {
			_item1Exp = Extract(context, _item1Exp);
			_item2Exp = Extract(context, _item2Exp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_item1Exp, _item2Exp);
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", _item1Exp, _item2Exp);
		}

	}

}