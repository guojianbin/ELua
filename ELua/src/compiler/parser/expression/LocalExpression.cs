namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LocalExpression : Expression {

        public string value;

        public LocalExpression(Expression exp) {
            value = ((WordExpression)exp).value;
            debugInfo = exp.debugInfo;
        }

        public override void Generate(ModuleContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Local, opArg1 = context.vm.GetString(value) });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(debugInfo);
        }

        public override string ToString() {
            return value;
        }

    }

}