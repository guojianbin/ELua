using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfExpression : Expression {

	    public Expression condExp;
	    public Expression moduleExp;

        public IfExpression(List<Expression> list, int position, int len) {
            isStatement = true;
            type = Type.If;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
            var itemsList = list.Skip(position + 3).Take(len - 4).ToList();
			moduleExp = new ModuleExpression(itemsList);
        }

		public IfExpression(Expression condExp, Expression moduleExp) {
			isStatement = true;
			type = Type.If;
			debugInfo = condExp.debugInfo;
		    this.condExp = condExp;
		    this.moduleExp = moduleExp;
	    }

	    public override void Extract(SyntaxContext context) {
            condExp = ParserHelper.Extract(context, condExp);
            moduleExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            condExp.Generate(context);
            var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = jumpEnd });
			moduleExp.Generate(context);
            endLabel.Generate(context);
	        jumpEnd.index = endLabel.index;
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(condExp, moduleExp);
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nend", condExp, moduleExp);
        }

    }

}