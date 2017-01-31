namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class OperatorExpression : Expression {

        public string value;

        public OperatorExpression(string value, DebugInfo debugInfo) {
            isFinally = true;
            type = Type.Operator;
            this.value = value;
            this.debugInfo = debugInfo;
        }

        public override string ToString() {
            return value;
        }

    }

}