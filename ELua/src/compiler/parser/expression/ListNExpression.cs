using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ListNExpression : Expression {

		public List<Expression> itemsList;

		public ListNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.List;
			debugInfo = list[position].debugInfo;
			itemsList = new List<Expression>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 2) {
				itemsList.Add(list[position + i + 1]);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
        }

        public override void Generate(ModuleContext context) {
            for (var i = itemsList.Count - 1; i >= 0; i--) {
                itemsList[i].Generate(context);
			}
			context.Add(new ByteCode { opCode = ByteCode.OpCode.List, opArg = new LuaInteger(context.vm, itemsList.Count) });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", itemsList.FormatListString());
		}

	}

}