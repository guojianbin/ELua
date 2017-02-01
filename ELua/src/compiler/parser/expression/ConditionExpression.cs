using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class ConditionExpression : Expression {

        public List<Expression> itemsList;

        public ConditionExpression(List<Expression> itemsList) {
            isModule = true;
            type = Type.Module;
            this.itemsList = itemsList;
            debugInfo = itemsList[0].debugInfo;
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
            context.RemoveAt(context.Count - 1);
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(itemsList.ToArray());
        }

        public override string ToString() {
            return itemsList.FormatListString("; ");
        }

    }

}