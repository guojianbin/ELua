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

        public override void Generate(ByteCodeContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaObject { IsNil = true } });
        }

        public override string ToString() {
            return "nil";
        }

    }

}