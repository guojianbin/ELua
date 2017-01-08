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

        public override void Generate(ILContext context) {
            context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaBoolean { value = bool.Parse(value)} });
        }

        public override string ToString() {
            return value;
        }

    }

}