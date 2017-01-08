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

        public override void Generate(ILContext context) {
            context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaObject { IsNil = true } });
        }

        public override string ToString() {
            return "nil";
        }

    }

}