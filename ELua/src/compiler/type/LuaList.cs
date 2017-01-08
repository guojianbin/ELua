using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaList : LuaObject {

        public LuaTable table;
        public List<LuaObject> list = new List<LuaObject>();

        public LuaObject this[int index] {
            set { list[index] = value; }
            get { return list[index]; }
        }

        public void Add(LuaObject item) {
            list.Add(item);
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            var index = (int)obj.ToNumber(stackFrame).value - 1;
            var value = list.ElementAtOrDefault(index);
            if (value == null) {
                return stackFrame.vm.nil;
            } else {
                return new LuaListItem { table = table, list = this, index = index, value = value };
            }
        }

        public override string ToString(StackFrame stackFrame) {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var item in list) {
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