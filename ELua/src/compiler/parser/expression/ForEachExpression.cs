using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ForEachExpression : Expression {

		public List<Expression> items1List;
		public List<Expression> items2List;
		public ModuleExpression moduleExp;

		public ForEachExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.ForEach;
			debugInfo = list[position].debugInfo;
			items1List = list.Skip(position + 1).TakeWhile(t => !ParserHelper.IsKeyword(t, "in")).Where(t => t.type == Type.Word).ToList();
			items2List = list.Skip(position + items1List.Count * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "do")).Where(t => t.IsRightValue).ToList();
			var itemsList = list.Skip(position + 1 + items1List.Count * 2 + items2List.Count * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var funcExp = new WordExpression(context.NewUID(), debugInfo);
			var statExp = new WordExpression(context.NewUID(), debugInfo);
			var varExp = new WordExpression(context.NewUID(), debugInfo);
			var initExp = new DefineNExpression(new List<Expression> { funcExp, statExp, varExp }, items2List);
			initExp.Generate(context);
			var whileExp = new WhileExpression(new BooleanExpression(true, debugInfo), moduleExp);
			var callExp = new CallNExpression(funcExp, new List<Expression> { statExp, varExp });
			var nextExp = new DefineNExpression(items1List, new List<Expression> { callExp });
            var rebindExp  = new BindExpression(varExp, items1List[0]);
			var breakExp = new BreakExpression();
			var exitExp = new IfExpression(new EqualExpression(varExp, new NilExpression()), new ModuleExpression(new List<Expression> { breakExp }));
			moduleExp.itemsList.InsertRange(0, new Expression[] { nextExp, rebindExp, exitExp });
			whileExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("for {0} in {1} do\n{2}\nend", string.Join(", ", items1List.Select(t => t.ToString())), string.Join(", ", items2List.Select(t => t.ToString())), moduleExp);
		}

	}

}