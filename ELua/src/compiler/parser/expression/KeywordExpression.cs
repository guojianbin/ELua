namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class KeywordExpression : Expression {

        public string value;

        public KeywordExpression(string value, DebugInfo debugInfo) {
            isFinally = true;
            type = Type.Keyword;
            this.value = value;
            this.debugInfo = debugInfo;
        }

        public override string ToString() {
            return value;
        }

    }

}