using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfElseExpression : Expression {

        public Expression condExp;
		public Expression module1Exp;
		public Expression module2Exp;

        public IfElseExpression(List<Expression> list, int position, int len) {
            isStatement = true;
            type = Type.IfElse;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
			module1Exp = list[position + 3];
			module2Exp = list[position + 5];
        }

		public IfElseExpression(Expression condExp, Expression item1Exp, Expression item2Exp) {
			isStatement = true;
			type = Type.IfElse;
			debugInfo = condExp.debugInfo;
			this.condExp = condExp;
			module1Exp = new ModuleExpression(new List<Expression> { item1Exp });
			module2Exp = new ModuleExpression(new List<Expression> { item2Exp });
		}

	    public override void Extract(SyntaxContext context) {
            condExp = ParserHelper.Extract(context, condExp);
            module1Exp.Extract(context);
			module2Exp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            condExp.Generate(context);
            var elseLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
            var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
	        var jumpElse = new LuaLabel(context.vm, elseLabel.value, elseLabel.index);
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = jumpElse });
			module1Exp.Generate(context);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg = jumpEnd });
            elseLabel.Generate(context);
	        jumpElse.index = elseLabel.index;
			module2Exp.Generate(context);
            endLabel.Generate(context);
	        jumpEnd.index = endLabel.index;
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(condExp, module1Exp, module2Exp);
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nelse\n{2}\nend", condExp, module1Exp, module2Exp);
        }

    }

}