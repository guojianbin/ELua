using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaList {

	    public LVM vm;
        public LuaTable table;
		public List<LuaListItem> itemsList = new List<LuaListItem>();

	    public int Count {
		    get { return itemsList.Count; }
	    }

	    public LuaList(LVM vm, LuaTable table) {
		    this.vm = vm;
		    this.table = table;
	    }

        public void Add(LuaObject value) {
            itemsList.Add(vm.GetListItem(table, this, Count, value));
        }

        public LuaObject GetIndex(LuaObject obj) {
            var index = (int)obj.ToNumber().value - 1;
	        if (index < 0) {
	            return table.GetProperty(obj);
	        } else if (index < Count) {
	            return itemsList[index];
	        } else {
                var len = index - Count + 1;
                for (var i = 0; i < len; i++) {
                    Add(vm.nil);
                }
                return itemsList[index];
            }
        }

	    public void Clear() {
		    itemsList.Clear();
	    }

	    public IEnumerator<LuaListItem> GetEnumerator() {
		    return itemsList.GetEnumerator();
	    }

		public override string ToString() {
			var sb = new StringBuilder();
			sb.Append('{');
			foreach (var item in itemsList) {
				sb.Append(item.value);
				sb.Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append('}');
			return sb.ToString();
        }

    }

}