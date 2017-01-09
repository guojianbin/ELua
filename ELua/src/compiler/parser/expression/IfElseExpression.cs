using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfElseExpression : Expression {

        public Expression condExp;
        public List<Expression> items1List;
        public List<Expression> items2List;

        public IfElseExpression(List<Expression> list, int position, int len) {
            IsStatement = true;
            type = Type.IfElse;
            debugInfo = list[position].debugInfo;
            condExp = list[position + 1];
            items1List = new List<Expression>();
            items2List = new List<Expression>();
            for (var i = position + 3; !ParserHelper.IsKeyword(list[i], "else"); i++) {
                items1List.Add(list[i]);
            }
            for (var i = position + 4 + items1List.Count; !ParserHelper.IsKeyword(list[i], "end"); i++) {
                items2List.Add(list[i]);
            }
        }

        public override void Extract(SyntaxContext context) {
            condExp = ParserHelper.Extract(context, condExp);
            var context1 = new SyntaxContext { level = context.level + 1, uid = context.uid, list = new List<Expression>() };
            foreach (var item in items1List) {
                item.Extract(context1);
                context1.Add(item);
            }
            items1List = context1.list;
            context.uid = context1.uid;
            var context2 = new SyntaxContext { level = context.level + 1, uid = context.uid, list = new List<Expression>() };
            foreach (var item in items2List) {
                item.Extract(context2);
                context2.Add(item);
            }
            items2List = context2.list;
            context.uid = context2.uid;
        }

        public override void Generate(ByteCodeContext context) {
            condExp.Generate(context);
            var elseExp = new LabelExpression(context.NewUID(), condExp.debugInfo);
            var endExp = new LabelExpression(context.NewUID(), condExp.debugInfo);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.JumpNot, opArg = new LuaLabel { value = elseExp.value } });
            foreach (var item in items1List) {
                item.Generate(context);
                context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            }
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Jump, opArg = new LuaLabel { value = endExp.value } });
            elseExp.Generate(context);
            foreach (var item in items2List) {
                item.Generate(context);
                context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
            }
            endExp.Generate(context);
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(new[] { condExp }.Concat(items1List).Concat(items2List).ToArray());
        }

        public override string ToString() {
            return string.Format("if {0} then\n{1}\nelse\n{2}\nend", condExp, string.Join("\n", items1List.Select(t => t.ToString())), string.Join("\n", items2List.Select(t => t.ToString())));
        }

    }

}