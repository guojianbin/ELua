using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ReturnNExpression : Expression {

		public List<Expression> itemsList;

		public ReturnNExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Return;
			debugInfo = list[position].debugInfo;
			itemsList = list.Skip(position + 1).Take(len - 1).Where(t => !ParserHelper.IsOperator(t, ",")).ToList();
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
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Return });
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return string.Format("return {0}", itemsList.FormatListString());
		}

	}

}