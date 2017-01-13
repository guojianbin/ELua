using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WhileExpression : Expression {

		public Expression condExp;
		public Expression moduleExp;

		public WhileExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.While;
			debugInfo = list[position].debugInfo;
			condExp = list[position + 1];
			var itemsList = list.Skip(position + 3).Take(len - 4).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public WhileExpression(Expression condExp, Expression moduleExp) {
			IsStatement = true;
			type = Type.While;
			debugInfo = condExp.debugInfo;
			this.condExp = condExp;
			this.moduleExp = moduleExp;
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
            context.ClearBreak();
			var beginLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
			beginLabel.Generate(context);
			var jumpBegin = new LuaLabel(context.vm, beginLabel.value, beginLabel.index);
			condExp.Generate(context);
			var endLabel = new LabelExpression(context.NewUID(), condExp.debugInfo);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg1 = jumpEnd });
			moduleExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg1 = jumpBegin });
			endLabel.Generate(context);
			jumpEnd.index = endLabel.index;
            context.ResetBreak(jumpEnd);
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(condExp, moduleExp);
		}

		public override string ToString() {
			return string.Format("while {0} do\n{1}\nend", condExp, moduleExp);
		}

	}

}