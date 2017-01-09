namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LabelExpression : Expression {

        public string value;

        public LabelExpression(string value, DebugInfo debugInfo) {
            IsStatement = true;
            type = Type.Label;
            this.value = value;
            this.debugInfo = debugInfo;
        }

        public override void Generate(ByteCodeContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Label, opArg = new LuaLabel { value = value } });
        }

        public override string ToString() {
            return string.Format("{0}:", value);
        }

    }

}