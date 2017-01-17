using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineNExpression : Expression {

		public List<Expression> items1List;
		public List<Expression> items2List;

		public DefineNExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Define;
			debugInfo = list[position].debugInfo;
			items1List = list.Skip(position + 1).TakeWhile(t => !ParserHelper.IsOperator(t, "=")).Where(t => t.type == Type.Word).Select(t => new LocalExpression(t)).Cast<Expression>().ToList();
			items2List = list.Skip(position + items1List.Count * 2).Take(len - items1List.Count * 2).Where(t => t.IsRightValue).ToList();
		}

		public DefineNExpression(List<Expression> items1List, List<Expression> items2List) {
			IsStatement = true;
			type = Type.Define;
			debugInfo = items1List[0].debugInfo;
			this.items1List = items1List.Select(t => new LocalExpression(t)).Cast<Expression>().ToList();
			this.items2List = items2List;
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < items2List.Count; i++) {
				items2List[i] = ParserHelper.Extract(context, items2List[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = items2List.Count - 1; i >= 0; i--) {
				items2List[i].Generate(context);
			}
			for (var i = items1List.Count - 1; i >= 0; i--) {
                items1List[i].Generate(context);
            }
			context.Add(new ByteCode { opCode = ByteCode.OpCode.BindN, opArg = new LuaInteger(context.vm, items1List.Count) });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(items1List.Concat(items2List).ToArray());
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", items1List.FormatListString(), items2List.FormatListString());
		}

	}

}