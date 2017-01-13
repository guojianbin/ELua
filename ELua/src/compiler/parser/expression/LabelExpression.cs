namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LabelExpression : Expression {

        public string value;
	    public int index;

        public LabelExpression(string value, DebugInfo debugInfo) {
            IsStatement = true;
            type = Type.Label;
            this.value = value;
            this.debugInfo = debugInfo;
        }

        public override void Generate(ModuleContext context) {
	        index = context.Count;
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Label, opArg1 = new LuaLabel(context.vm, value, index) });
        }

        public override string ToString() {
            return string.Format("{0}:", value);
        }

    }

}