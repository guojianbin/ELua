using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaList : LuaObject {

        public LuaTable table;
        public List<LuaObject> itemsList = new List<LuaObject>();

        public LuaObject this[int index] {
            set { itemsList[index] = value; }
            get { return itemsList[index]; }
        }

        public void Add(LuaObject item) {
            itemsList.Add(item);
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            var index = (int)obj.ToNumber(stackFrame).value - 1;
            var value = itemsList.ElementAtOrDefault(index);
            if (value == null) {
                return stackFrame.nil;
            } else {
                return new LuaListItem { table = table, list = this, index = index, value = value };
            }
        }

        public override string ToString(StackFrame stackFrame) {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var item in itemsList) {
                sb.Append(item.ToString(stackFrame));
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append('}');
            return sb.ToString();
        }

        public override string ToString() {
            return "list";
        }

    }

}