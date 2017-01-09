using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleExpression : Expression {

		public List<Expression> itemsList;

		public ModuleExpression(List<Expression> list, int position, int len) {
			IsChunked = true;
			type = Type.Module;
			debugInfo = list[position].debugInfo;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(list[position + i]);
			}
		}

		public override void Extract(SyntaxContext context) {
            context = new SyntaxContext { list = new List<Expression>() };
            foreach (var item in itemsList) {
                item.Extract(context);
                context.Add(item);
            }
            itemsList = context.list;
		}

		public override void Generate(ByteCodeContext context) {
            foreach (var item in itemsList) {
                item.Generate(context);
                context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            }
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return string.Join("\n", itemsList.Select(t => t.ToString()));
		}

	}

}