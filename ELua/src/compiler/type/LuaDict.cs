using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaDict {

        public LuaTable table;
		public Dictionary<LuaObject, LuaDictItem> itemsDict = new Dictionary<LuaObject, LuaDictItem>();

		public int Length {
			get { return itemsDict.Count; }
		}

	    public LuaObject Bind(LuaObject key, LuaObject value) {
			return itemsDict[key] = new LuaDictItem { table = table, dict = this, key = key, value = value };
	    }

		public LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
			return GetProperty(stackFrame, obj);
		}

        public LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
	        LuaDictItem value;
	        if (!itemsDict.TryGetValue(obj, out value)) {
		        return stackFrame.nil;
	        } else {
		        return value;
	        }
        }

	    public override string ToString() {
			var sb = new StringBuilder();
			sb.Append('{');
			foreach (var item in itemsDict.Values) {
				sb.Append(item.key);
				sb.Append(':');
				sb.Append(item.value);
				sb.Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append('}');
			return sb.ToString();
        }

    }

}