using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class OrExpression : Expression {

        public Expression item1Exp;
        public Expression item2Exp;

        public OrExpression(List<Expression> list, int position, int len) {
            isRightValue = true;
            type = Type.Or;
            debugInfo = list[position].debugInfo;
            item1Exp = list[position];
            item2Exp = list[position + 2];
        }

		public override void Extract(SyntaxContext context) {
			item1Exp = ParserHelper.Extract(context, item1Exp);
			var itemKey = context.NewUID();
			var step1Item = ParserHelper.Wrapper(item1Exp, itemKey);
			var step2Item = ParserHelper.Wrapper(item2Exp, itemKey);
			var condExp = new IfElseExpression(item1Exp, step1Item.Value, step2Item.Value);
			context.Add(condExp);
			context.isCutting = true;
			context.cuttingExp = step1Item.Key;
        }

        public override void Generate(ModuleContext context) {
            item2Exp.Generate(context);
            item1Exp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Or });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(item1Exp, item2Exp);
        }

        public override string ToString() {
            return string.Format("{0} or {1}", item1Exp, item2Exp);
        }

    }

}