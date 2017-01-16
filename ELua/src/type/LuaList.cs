﻿using System.Collections.Generic;
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

	    public void Bind(int index, LuaObject value) {
			itemsList[index] = vm.GetListItem(table, this, index, value);
	    }

        public void Add(LuaObject value) {
            itemsList.Add(vm.GetListItem(table, this, Count, value));
        }

	    public LuaListItem IndexOf(int index) {
			return itemsList.ElementAtOrDefault(index);
	    }

	    public LuaObject GetIndex(LuaObject obj) {
            var index = (int)obj.ToNumber().value - 1;
		    var value = IndexOf(index);
		    if (value == null) {
			    return vm.GetListItem(table, this, index, vm.nil);
		    } else {
				return value;
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