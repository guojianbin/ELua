using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleExpression : Expression {

		public List<Expression> itemsList;

		public ModuleExpression(List<Expression> list, int position, int len) {
			isModule = true;
			type = Type.Module;
			debugInfo = list[position].debugInfo;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(list[position + i]);
			}
		}

		public ModuleExpression(List<Expression> itemsList) {
			isModule = true;
			type = Type.Module;
			this.itemsList = itemsList;
            if (itemsList.Count > 0) {
                debugInfo = itemsList[0].debugInfo;
            }
		}

		public override void Extract(SyntaxContext context) {
			context = new SyntaxContext(context.parser, context.level + 1);
			foreach (var item in itemsList) {
				item.Extract(context);
				context.Add(item);
			}
			itemsList = context.list;
		}

		public override void Generate(ModuleContext context) {
			foreach (var item in itemsList) {
				item.Generate(context);
				context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString("\n");
		}

	}

}