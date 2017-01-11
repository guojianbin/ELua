using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfElseExpression : Expression {

        public Expression condExp;
	    public ChunkExpression chunk1Exp;
	    public ChunkExpression chunk2Exp;

        public IfElseExpression(List<Expression> list, int position, int len) {
            IsStatement = true;
            type = Type.IfElse;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
	        var items1List = list.Skip(position + 3).TakeWhile(t => !ParserHelper.IsKeyword(t, "else")).ToList();
			var items2List = list.Skip(position + 4 + items1List.Count).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			chunk1Exp = new ChunkExpression(items1List);
			chunk2Exp = new ChunkExpression(items2List);
        }

		public IfElseExpression(Expression condExp, Expression item1Exp, Expression item2Exp) {
			IsStatement = true;
			type = Type.IfElse;
			debugInfo = condExp.debugInfo;
			this.condExp = condExp;
			chunk1Exp = new ChunkExpression(new List<Expression> { item1Exp });
			chunk2Exp = new ChunkExpression(new List<Expression> { item2Exp });
		}

		public IfElseExpression(Expression condExp, List<Expression> items1List, List<Expression> items2List) {
			IsStatement = true;
			type = Type.IfElse;
			debugInfo = condExp.debugInfo;
		    this.condExp = condExp;
			chunk1Exp = new ChunkExpression(items1List);
			chunk2Exp = new ChunkExpression(items2List);
	    }

	    public override void Extract(SyntaxContext context) {
            condExp = ParserHelper.Extract(context, condExp);
			chunk1Exp.Extract(context);
			chunk2Exp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
            condExp.Generate(context);
            var elseLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
            var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
	        var jumpElse = new LuaLabel { value = elseLabel.value, index = elseLabel.index};
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg1 = jumpElse });
			chunk1Exp.Generate(context);
	        var jumpEnd = new LuaLabel { value = endLabel.value, index = endLabel.index };
	        context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg1 = jumpEnd });
            elseLabel.Generate(context);
	        jumpElse.index = elseLabel.index;
			chunk2Exp.Generate(context);
            endLabel.Generate(context);
	        jumpEnd.index = endLabel.index;
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(condExp, chunk1Exp, chunk2Exp);
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nelse\n{2}\nend", condExp, chunk1Exp, chunk2Exp);
        }

    }

}