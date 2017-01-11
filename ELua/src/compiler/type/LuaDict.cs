using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaDict {

        public LuaTable table;
        public Dictionary<LuaObject, LuaObject> itemsDict = new Dictionary<LuaObject, LuaObject>();

	    public LuaObject Bind(LuaObject key, LuaObject value) {
		    return itemsDict[key] = value;
	    }

		public LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
			return GetProperty(stackFrame, obj);
		}

        public LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            LuaObject value;
	        if (itemsDict.TryGetValue(obj, out value)) {
                return new LuaDictItem { table = table, dict = this, key = obj, value = value };
            } else {
                return stackFrame.nil;
            }
        }

		public override string ToString() {
			var sb = new StringBuilder();
			sb.Append('{');
			foreach (var item in itemsDict) {
				sb.Append(item.Key);
				sb.Append(':');
				sb.Append(item.Value);
				sb.Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append('}');
			return sb.ToString();
        }

    }

}