using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class IfCondExpression : Expression {

        private Expression _condExp;
        private List<Expression> _itemsList;

        public IfCondExpression(List<Expression> list, int position, int len) {
            IsStatement = true;
            type = Type.If;
            debugInfo = list[position].debugInfo;
            _condExp = list[position + 1];
            _itemsList = new List<Expression>();
            var itemLen = len - 4;
            for (var i = 0; i < itemLen; i++) {
                _itemsList.Add(list[position + i + 3]);
            }
        }

        public override void Extract(SyntaxContext context) {
            _condExp = ParserHelper.Extract(context, _condExp);
            for (var i = 0; i < _itemsList.Count; i++) {
                _itemsList[i] = ParserHelper.Extract(context, _itemsList[i]);
            }
        }

        public override void Generate(ILContext context) {
            for (var i = _itemsList.Count - 1; i >= 0; i--) {
                _itemsList[i].Generate(context);
            }
            _condExp.Generate(context);
            context.Add(new IL { opCode = IL.OpCode.JumpIf });
        }

        public override string GetDebugInfo() {
            return DebugInfo.ToString(new[] { _condExp }.Concat(_itemsList).ToArray());
        }

        public override string ToString() {
            return string.Format("if {0} then {1} end", _condExp, string.Join(" ", _itemsList.Select(t => t.ToString())));
        }

    }

}