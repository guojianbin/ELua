namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class NilExpression : Expression {

        public NilExpression(DebugInfo debugInfo) {
            IsFinally = true;
            type = Type.Nil;
            IsRightValue = true;
            this.debugInfo = debugInfo;
        }

	    public NilExpression() {
		    // ignored
	    }

	    public override void Generate(ModuleContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = context.vm.nil });
        }

        public override string ToString() {
            return "nil";
        }

    }

}