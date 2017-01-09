using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfExpression : Expression {

	    public Expression condExp;
	    public List<Expression> itemsList;

        public IfExpression(List<Expression> list, int position, int len) {
            IsStatement = true;
            type = Type.If;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
            itemsList = new List<Expression>();
            var itemLen = len - 4;
            for (var i = 0; i < itemLen; i++) {
                itemsList.Add(list[position + i + 3]);
            }
        }

        public override void Extract(SyntaxContext context) {
            context = new SyntaxContext { level = context.level + 1, uid = context.uid, list = new List<Expression>() };
            condExp = ParserHelper.Extract(context, condExp);
            foreach (var item in itemsList) {
                item.Extract(context);
                context.Add(item);
            }
            itemsList = context.list;
        }

        public override void Generate(ByteCodeContext context) {
            condExp.Generate(context);
            var endExp = new LabelExpression(context.NewUID(), condExp.debugInfo);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = new LuaLabel { value = endExp.value } });
            for (var i = 0; i < itemsList.Count; i++) {
                itemsList[i].Generate(context);
                context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            }
            endExp.Generate(context);
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(new[] { condExp }.Concat(itemsList).ToArray());
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nend", condExp, string.Join("\n", itemsList.Select(t => t.ToString())));
        }

    }

}