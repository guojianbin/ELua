using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public DefineExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Define;
			debugInfo = list[position].debugInfo;
			item1Exp = list[position + 1];
			item2Exp = list[position + 3];
		}

		public DefineExpression(Expression item1Exp, Expression item2Exp) {
			IsStatement = true;
            type = Type.Define;
		    debugInfo = item1Exp.debugInfo;
            this.item1Exp = item1Exp;
			this.item2Exp = item2Exp;
        }

        public override void Extract(SyntaxContext context) {
            item2Exp = ParserHelper.Extract(context, item2Exp);
        }

        public override void Generate(ModuleContext context) {
			item2Exp.Generate(context);
            item1Exp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(item1Exp, item2Exp);
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", item1Exp, item2Exp);
		}

	}

}