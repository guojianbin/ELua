using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaList {

        public LuaTable table;
		public List<LuaListItem> itemsList = new List<LuaListItem>();

	    public int Length {
		    get { return itemsList.Count; }
	    }

	    public void Bind(int index, LuaObject value) {
			var item = new LuaListItem { table = table, list = this, index = index, value = value };
		    itemsList[index] = item;
	    }

        public void Add(LuaObject value) {
	        var item = new LuaListItem { table = table, list = this, index = itemsList.Count, value = value };
            itemsList.Add(item);
        }

        public LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            var index = (int)obj.ToNumber(stackFrame).value - 1;
            var value = itemsList.ElementAtOrDefault(index);
            if (value == null) {
                return stackFrame.nil;
            } else {
                return value;
            }
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