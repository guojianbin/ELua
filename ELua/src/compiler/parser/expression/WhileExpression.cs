using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WhileExpression : Expression {

		public Expression condExp;
		public Expression moduleExp;

		public WhileExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.While;
			debugInfo = list[position].debugInfo;
            condExp = new ConditionExpression(new List<Expression> { list[position + 1] });
            moduleExp = list[position + 3];
        }

		public override void Extract(SyntaxContext context) {
            condExp.Extract(context);
            moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
            context.BeginLoop();
			var beginLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
			beginLabel.Generate(context);
			var jumpBegin = new LuaLabel(context.vm, beginLabel.value, beginLabel.index);
			condExp.Generate(context);
			var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = jumpEnd });
			moduleExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg = jumpBegin });
			endLabel.Generate(context);
			jumpEnd.index = endLabel.index;
            context.EndLoop(jumpEnd);
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(condExp, moduleExp);
		}

		public override string ToString() {
			return string.Format("while {0} do\n{1}\nend", condExp, moduleExp);
		}

	}

}