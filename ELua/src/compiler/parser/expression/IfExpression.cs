using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfExpression : Expression {

	    public Expression condExp;
	    public ChunkExpression chunkExp;

        public IfExpression(List<Expression> list, int position, int len) {
            IsStatement = true;
            type = Type.If;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
            var itemsList = list.Skip(position + 3).Take(len - 4).ToList();
			chunkExp = new ChunkExpression(itemsList);
        }

		public override void Extract(SyntaxContext context) {
			condExp = ParserHelper.Extract(context, condExp);
			chunkExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            condExp.Generate(context);
            var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
	        var jumpEnd = new LuaLabel { value = endLabel.value, index = endLabel.index };
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = jumpEnd });
			chunkExp.Generate(context);
            endLabel.Generate(context);
	        jumpEnd.index = endLabel.index;
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(condExp, chunkExp);
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nend", condExp, chunkExp);
        }

    }

}