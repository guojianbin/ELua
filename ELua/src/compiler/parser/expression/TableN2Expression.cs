using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TableN2Expression : Expression {

		public List<KeyValuePair<Expression, Expression>> itemsList;

		public TableN2Expression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Table;
			debugInfo = list[position].debugInfo;
			itemsList = new List<KeyValuePair<Expression, Expression>>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 6) {
				itemsList.Add(new KeyValuePair<Expression, Expression>(list[position + i + 2], list[position + i + 5]));
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				var item = itemsList[i];
				var itemKey = ParserHelper.Extract(context, item.Key);
				var itemValue = ParserHelper.Extract(context, item.Value);
				itemsList[i] = new KeyValuePair<Expression, Expression>(itemKey, itemValue);
			}
        }

        public override void Generate(ModuleContext context) {
            for (var i = itemsList.Count - 1; i >= 0; i--) {
                var item = itemsList[i];
                item.Value.Generate(context);
                item.Key.Generate(context);
			}
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Table, opArg = new LuaInteger(context.vm, itemsList.Count * 2) });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.Select(t => new[] { t.Key, t.Value }).SelectMany(t => t).ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", itemsList.Select(t => string.Format("[{0}] = {1}", t.Key, t.Value)).FormatListString());
		}

	}

}