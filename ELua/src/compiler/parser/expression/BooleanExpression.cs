namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class BooleanExpression : Expression {

        public string value;

        public BooleanExpression(string value, DebugInfo debugInfo) {
            IsFinally = true;
            type = Type.Boolean;
            IsRightValue = true;
            this.value = value;
            this.debugInfo = debugInfo;
        }

        public override void Generate(ByteCodeContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaBoolean { value = bool.Parse(value)} });
        }

        public override string ToString() {
            return value;
        }

    }

}