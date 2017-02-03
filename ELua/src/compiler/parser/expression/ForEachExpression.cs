using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ForEachExpression : Expression {

		public List<Expression> items1List;
		public List<Expression> items2List;
		public ModuleExpression moduleExp;

		public ForEachExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.ForEach;
			debugInfo = list[position].debugInfo;
			items1List = ((LeftList2Expression)list[position + 1]).itemsList;
			items2List = ((RightList2Expression)list[position + 3]).itemsList;
			moduleExp = ((ModuleExpression)list[position + 5]);
        }

        public override void Extract(SyntaxContext context) {
			for (var i = 0; i < items2List.Count; i++) {
				items2List[i] = ParserHelper.Extract(context, items2List[i]);
			}
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var funcExp = new WordExpression(context.NewUID(), debugInfo);
			var statExp = new WordExpression(context.NewUID(), debugInfo);
			var varExp = new WordExpression(context.NewUID(), debugInfo);
			var initExp = new DefineNExpression(new List<Expression> { funcExp, statExp, varExp }, items2List);
			var whileExp = new LoopExpression(moduleExp);
            var retExp = new WordExpression(context.NewUID(), debugInfo);
            var retDef = new DefineExpression(retExp, new CallExpression(funcExp, new RightList1Expression(new List<Expression> { statExp, varExp })));
            var callExp = new UnpackExpression(retExp);
            var nextExp = new BindNExpression(new LeftList2Expression(items1List), new RightList2Expression(new List<Expression> { callExp }));
            var rebindExp  = new BindExpression(varExp, items1List[0]);
			var breakExp = new BreakExpression();
			var exitExp = new IfExpression(new EqualExpression(varExp, new NilExpression()), new ModuleExpression(new List<Expression> { breakExp }));
			moduleExp.itemsList.InsertRange(0, new Expression[] { retDef, nextExp, rebindExp, exitExp });
			initExp.Generate(context);
			whileExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("for {0} in {1} do\n{2}\nend", items1List.FormatListString(), items2List.FormatListString(), moduleExp);
		}

	}

}