using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class ForExpression : Expression {

        public Expression indexExp;
        public Expression beginExp;
        public Expression endExp;
        public Expression stepExp;
        public Expression condExp;
        public Expression changeExp;
        public Expression moduleExp;

        public ForExpression(List<Expression> list, int position, int len) {
            isStatement = true;
            type = Type.For;
            debugInfo = list[position].debugInfo;
            indexExp = list[position + 1];
            beginExp = list[position + 3];
            endExp = list[position + 5];
            stepExp = new NumberExpression(1, debugInfo);
            condExp = new GreaterExpression(indexExp, endExp);
            changeExp = new BindExpression(indexExp, new AddExpression(indexExp, stepExp));
            moduleExp = list[position + 7];
        }

        public override void Extract(SyntaxContext context) {
            beginExp = ParserHelper.Extract(context, beginExp);
            endExp = ParserHelper.Extract(context, endExp);
            moduleExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            context.BeginLoop();
            var bindIndex = new BindExpression(indexExp, beginExp);
            bindIndex.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            var beginLabel = new LabelExpression(context.NewUID(), indexExp.debugInfo);
            beginLabel.Generate(context);
			var jumpBegin = new LuaLabel(context.vm, beginLabel.value, beginLabel.index);
            condExp.Generate(context);
            var endLabel = new LabelExpression(context.NewUID(), indexExp.debugInfo);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpIf, opArg = jumpEnd });
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            moduleExp.Generate(context);
            changeExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg = jumpBegin });
            endLabel.Generate(context);
			jumpEnd.index = endLabel.index;
            context.EndLoop(jumpEnd);
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(indexExp, beginExp, endExp, moduleExp);
        }

        public override string ToString() {
            return string.Format("for {0}={1},{2} do\n{3}\nend", indexExp, beginExp, endExp, moduleExp);
        }

    }

}