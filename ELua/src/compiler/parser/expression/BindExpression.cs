using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BindExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public BindExpression(List<Expression> list, int position, int len) {
			type = Type.Bind;
			debugInfo = list[position].debugInfo;
			IsStatement = true;
			item1Exp = list[position];
			item2Exp = list[position + 2];
		}

	    public BindExpression(Expression item1Exp, Expression item2Exp) {
            type = Type.Bind;
            IsStatement = true;
            this.item1Exp = item1Exp;
	        this.item2Exp = item2Exp;
            debugInfo = item1Exp.debugInfo;
        }

	    public override void Extract(SyntaxContext context) {
			item1Exp = ParserHelper.Extract(context, item1Exp);
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
			return string.Format("{0} = {1}", item1Exp, item2Exp);
		}

	}

}