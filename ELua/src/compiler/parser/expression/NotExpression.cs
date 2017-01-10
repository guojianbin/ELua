using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class NotExpression : Expression {

        public Expression targetExp;

        public NotExpression(List<Expression> list, int position, int len) {
            IsRightValue = true;
            type = Type.Not;
            debugInfo = list[position].debugInfo;
            targetExp = list[position + 1];
        }

        public override void Extract(SyntaxContext context) {
            targetExp = ParserHelper.Extract(context, targetExp);
        }

        public override void Generate(ModuleContext context) {
            targetExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Not });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(targetExp);
        }

        public override string ToString() {
            return string.Format("not {0}", targetExp);
        }

    }

}