using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaList : LuaObject {

        public List<LuaObject> list = new List<LuaObject>();

        public void Add(LuaObject item) {
            list.Add(item);
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