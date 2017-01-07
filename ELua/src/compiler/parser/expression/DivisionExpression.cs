using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DivisionExpression : Expression {

		private Expression _item1Exp;
		private Expression _item2Exp;

		public DivisionExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Division;
			debugInfo = list[position].debugInfo;
			_item1Exp = list[position];
			_item2Exp = list[position + 2];
		}

		public override void Extract(SyntaxContext context) {
			_item1Exp = Extract(context, _item1Exp);
			_item2Exp = Extract(context, _item2Exp);
        }

        public override void Generate(ILContext context) {
            context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaNumber { value = float.Parse(_item2Exp.value) } });
            context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaNumber { value = float.Parse(_item1Exp.value) } });
            context.Add(new IL { opCode = IL.OpCode.Division });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(_item1Exp, _item2Exp);
		}

		public override string ToString() {
			return string.Format("{0} / {1}", _item1Exp, _item2Exp);
		}

	}

}