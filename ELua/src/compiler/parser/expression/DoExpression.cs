using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class DoExpression : Expression {

        public Expression moduleExp;

        public DoExpression(List<Expression> list, int position, int len) {
            isStatement = true;
            type = Type.Do;
            debugInfo = list[position].debugInfo;
            moduleExp = list[position + 1];
        }

        public override void Extract(SyntaxContext context) {
            moduleExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            moduleExp.Generate(context);
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(moduleExp);
        }

        public override string ToString() {
            return string.Format("do\n{0}\nend", moduleExp);
        }

    }

}