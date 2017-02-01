using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallNExpression : Expression {

		public Expression targetExp;
		public readonly List<Expression> itemsList;

		public CallNExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			isStatement = true;
            type = Type.Call;
			debugInfo = list[position].debugInfo;
			targetExp = list[position];
			itemsList = new List<Expression>();
			var itemLen = len - 2;
			for (var i = 0; i < itemLen; i += 2) {
				itemsList.Add(list[position + i + 2]);
			}
		}

		public CallNExpression(Expression targetExp, List<Expression> itemsList) {
			isRightValue = true;
			isStatement = true;
            type = Type.Call;
			debugInfo =targetExp.debugInfo;
			this.targetExp = targetExp;
			this.itemsList = itemsList;
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
		    for (var i = itemsList.Count - 1; i >= 0; i--) {
		        itemsList[i].Generate(context);
		    }
			targetExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Call });
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(new[] { targetExp }.Concat(itemsList).ToArray());
		}

		public override string ToString() {
			return string.Format("{0}({1})", targetExp, itemsList.FormatListString());
		}

	}

}