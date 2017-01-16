using System.Collections.Generic;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaDict {

	    public LVM vm;
        public LuaTable table;
		public Dictionary<LuaObject, LuaDictItem> itemsDict = new Dictionary<LuaObject, LuaDictItem>();

		public int Count {
			get { return itemsDict.Count; }
		}

		public LuaDict(LVM vm, LuaTable table) {
			this.vm = vm;
		    this.table = table;
	    }

	    public LuaObject Bind(LuaObject key, LuaObject value) {
			return itemsDict[key] = table.vm.GetDictItem(table, this, key, value);
	    }

		public LuaObject GetIndex(LuaObject obj) {
			return GetProperty(obj);
		}

        public LuaObject GetProperty(LuaObject obj) {
	        LuaDictItem value;
	        if (!itemsDict.TryGetValue(obj, out value)) {
                return Bind(obj, vm.nil);
	        } else {
		        return value;
	        }
        }

		public void Clear() {
			itemsDict.Clear();
		}

		public IEnumerator<LuaDictItem> GetEnumerator() {
			return itemsDict.Values.GetEnumerator();
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