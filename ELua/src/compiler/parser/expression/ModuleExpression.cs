using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModuleExpression : Expression {

		public ChunkExpression chunkExp;

		public ModuleExpression(List<Expression> list, int position, int len) {
			IsChunked = true;
			type = Type.Module;
			debugInfo = list[position].debugInfo;
			var itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(list[position + i]);
			}
		    if (itemsList.Count > 0) {
                chunkExp = new ChunkExpression(itemsList);
            } else {
		        chunkExp = new ChunkExpression(new List<Expression>());
		    }
		}

		public override void Extract(SyntaxContext context) {
			chunkExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			chunkExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return chunkExp.GetDebugInfo();
		}

		public override string ToString() {
			return chunkExp.ToString();
		}

	}

}