using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class PropertyExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public PropertyExpression(List<Expression> list, int position, int len) {
			isLeftValue = true;
			isRightValue = true;
			type = Type.Property;
			debugInfo = list[position].debugInfo;
			item1Exp = list[position];
			item2Exp = ParserHelper.Word2String(list[position + 2]);
		}

		public PropertyExpression(Expression item1Exp, Expression item2Exp) {
			isLeftValue = true;
			isRightValue = true;
			type = Type.Property;
			debugInfo = item1Exp.debugInfo;
			this.item1Exp = item1Exp;
			this.item2Exp = item2Exp;
		}

		public override void Extract(SyntaxContext context) {
			item1Exp = ParserHelper.Extract(context, item1Exp);
			item2Exp = ParserHelper.Extract(context, item2Exp);
        }

        public override void Generate(ModuleContext context) {
            item2Exp.Generate(context);
            item1Exp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Property });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(item1Exp, item2Exp);
		}

		public override string ToString() {
			return string.Format("{0}[{1}]", item1Exp, item2Exp);
		}

	}

}