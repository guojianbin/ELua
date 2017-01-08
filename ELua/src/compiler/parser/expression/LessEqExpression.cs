using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LessEqExpression : Expression {

        private Expression _item1Exp;
        private Expression _item2Exp;

        public LessEqExpression(List<Expression> list, int position, int len) {
            IsRightValue = true;
            type = Type.LessEq;
            debugInfo = list[position].debugInfo;
            _item1Exp = list[position];
            _item2Exp = list[position + 3];
        }

        public override void Extract(SyntaxContext context) {
            _item1Exp = ParserHelper.Extract(context, _item1Exp);
            _item2Exp = ParserHelper.Extract(context, _item2Exp);
        }

        public override void Generate(ILContext context) {
            _item2Exp.Generate(context);
            _item1Exp.Generate(context);
            context.Add(new IL { opCode = IL.OpCode.LessEq });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(_item1Exp, _item2Exp);
        }

        public override string ToString() {
            return string.Format("{0} <= {1}", _item1Exp, _item2Exp);
        }

    }

}