using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class CallExpression : Expression {

        public Expression targetExp;
        public Expression paramsExp;

        public CallExpression(List<Expression> list, int position, int len) {
            isRightValue = true;
            isStatement = true;
            type = Type.Call;
            debugInfo = list[position].debugInfo;
            targetExp = list[position];
	        paramsExp = list[position + 2];
        }

        public CallExpression(Expression targetExp, Expression paramsExp) {
            isRightValue = true;
            isStatement = true;
            type = Type.Call;
            this.targetExp = targetExp;
            this.paramsExp = paramsExp;
        }

        public override void Extract(SyntaxContext context) {
            targetExp = ParserHelper.Extract(context, targetExp);
			paramsExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
			paramsExp.Generate(context);
            targetExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Call });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp, paramsExp);
        }

        public override string ToString() {
			return string.Format("{0}({1})", targetExp, paramsExp);
        }

    }

}