using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ChunkExpression : Expression {

		public List<Expression> itemsList;

		public ChunkExpression(List<Expression> itemsList) {
			IsChunked = true;
			type = Type.Chunk;
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
			return string.Join("\n", itemsList.Select(t => t.ToString()));
		}

	}

}