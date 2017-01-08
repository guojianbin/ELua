using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public Dictionary<LuaObject, LuaObject> items;
        public List<LuaObject> list;
        public bool IsArray;
        public bool IsInit;

        public static LuaTable CreateList(LuaObject[] args) {
            var luaTable = new LuaTable { IsArray = true, IsInit = true, list = new List<LuaObject>() };
            for (var i = 0; i < args.Length; i ++) {
                luaTable.list.Add(args[i]);
            }
            return luaTable;
        }

        public static LuaTable CreateTable(LuaObject[] args) {
            var luaTable = new LuaTable { IsArray = false, IsInit = true, items = new Dictionary<LuaObject, LuaObject>() };
            for (var i = 0; i < args.Length; i += 2) {
                luaTable.items[args[i]] = args[i + 1];
            }
            return luaTable;
        }

        public override string ToString(StackFrame stackFrame) {
            if (!IsInit) {
                return "{ }";
            } else if (IsArray) {
                var sb = new StringBuilder();
                sb.Append('{');
                foreach (var item in list) {
                    sb.Append(item.ToString(stackFrame));
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append('}');
                return sb.ToString();
            } else {
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
        }

        public override string ToString() {
            return items.ToString();
        }

    }

}