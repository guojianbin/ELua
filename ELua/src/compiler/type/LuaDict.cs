using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaDict : LuaObject {

        public LuaTable table;
        public Dictionary<LuaObject, LuaObject> items = new Dictionary<LuaObject, LuaObject>();

        public LuaObject this[LuaObject key] {
            set { items[key] = value; }
            get { return items[key]; }
        }

        public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            LuaObject value;
            if (items.TryGetValue(obj, out value)) {
                return new LuaDictItem { table = table, dict = this, key = obj, value = value };
            } else {
                return stackFrame.vm.nil;
            }
        }

        public override string ToString(StackFrame stackFrame) {
            var sb = new StringBuilder();
            sb.Append('{');
            foreach (var item in items) {
                sb.Append(item.Key.ToString(stackFrame));
                sb.Append(':');
                sb.Append(item.Value.ToString(stackFrame));
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append('}');
            return sb.ToString();
        }

        public override string ToString() {
            return "dict";
        }

    }

}