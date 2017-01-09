using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class AndExpression : Expression {

        public Expression item1Exp;
        public Expression item2Exp;

        public AndExpression(List<Expression> list, int position, int len) {
            IsRightValue = true;
            type = Type.And;
            debugInfo = list[position].debugInfo;
            item1Exp = list[position];
            item2Exp = list[position + 2];
        }

        public override void Extract(SyntaxContext context) {
            item1Exp = ParserHelper.Extract(context, item1Exp);
            item2Exp = ParserHelper.Extract(context, item2Exp);
        }

        public override void Generate(ByteCodeContext context) {
            item2Exp.Generate(context);
            item1Exp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.And });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(item1Exp, item2Exp);
        }

        public override string ToString() {
            return string.Format("{0} and {1}", item1Exp, item2Exp);
        }

    }

}