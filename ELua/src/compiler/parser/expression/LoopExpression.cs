namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LoopExpression : Expression {

		public Expression moduleExp;

		public LoopExpression(Expression moduleExp) {
			isStatement = true;
			type = Type.Loop;
			debugInfo = moduleExp.debugInfo;
			this.moduleExp = moduleExp;
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			context.BeginLoop();
			var beginLabel = new LabelExpression(context.NewUID(), debugInfo);
			beginLabel.Generate(context);
			var jumpBegin = new LuaLabel(context.vm, beginLabel.value, beginLabel.index);
			var endLabel = new LabelExpression(context.NewUID(), debugInfo);
			var jumpEnd = new LuaLabel(context.vm, endLabel.value, endLabel.index);
			moduleExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg = jumpBegin });
			endLabel.Generate(context);
			jumpEnd.index = endLabel.index;
			context.EndLoop(jumpEnd);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("while true do\n{0}\nend", moduleExp);
		}

	}

}