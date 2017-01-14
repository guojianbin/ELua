using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static LuaUserdata GetUserdata(this LVM vm, object value) {
			return vm.luaPools.GetUserdata(value);
		}

		public static void PutUserdata(this LVM vm, LuaUserdata item) {
			vm.luaPools.PutUserdata(item);
		}

		public static LuaNumber GetNumber(this LVM vm, float value) {
			return vm.luaPools.GetNumber(value);
		}

		public static void PutNumber(this LVM vm, LuaNumber item) {
			vm.luaPools.PutNumber(item);
		}

		public static LuaBoolean GetBoolean(this LVM vm, bool value) {
			return vm.luaPools.GetBoolean(value);
		}

		public static void PutBoolean(this LVM vm, LuaBoolean item) {
			vm.luaPools.PutBoolean(item);
		}

		public static LuaString GetString(this LVM vm, string value) {
			return vm.luaPools.GetString(value);
		}

		public static void PutString(this LVM vm, LuaString item) {
			vm.luaPools.PutString(item);
		}

		public static LuaVar GetVar(this LVM vm, string value) {
			return vm.luaPools.GetVar(value);
		}

		public static void PutVar(this LVM vm, LuaVar item) {
			vm.luaPools.PutVar(item);
        }

        public static LuaFunction GetFunction(this LVM vm, Module value) {
            return vm.luaPools.GetFunction(value);
        }

        public static void PuFunction(this LVM vm, LuaFunction item) {
            vm.luaPools.PutFunction(item);
        }

        public static LuaTable GetTable(this LVM vm) {
			return vm.luaPools.GetTable();
		}

		public static void PutTable(this LVM vm, LuaTable item) {
			vm.luaPools.PutTable(item);
		}

		public static LuaTuple GetTuple(this LVM vm, IEnumerable<LuaObject> list) {
			return vm.luaPools.GetTuple(list);
		}

		public static void PutTuple(this LVM vm, LuaTuple item) {
			vm.luaPools.PutTuple(item);
		}

		public static LuaListItem GetListItem(this LVM vm, LuaTable table, LuaList list, int index, LuaObject value) {
			return vm.luaPools.GetListItem(table, list, index, value);
		}

		public static void PutListItem(this LVM vm, LuaListItem item) {
			vm.luaPools.PutListItem(item);
		}

		public static LuaDictItem GetDictItem(this LVM vm, LuaTable table, LuaDict dict, LuaObject key, LuaObject value) {
			return vm.luaPools.GetDictItem(table, dict, key, value);
		}

		public static void PutDictItem(this LVM vm, LuaDictItem item) {
			vm.luaPools.PutDictItem(item);
		}

		public static void ClearAll<T>(this Stack<T> stack) {
			while (stack.Count > 0) {
				stack.Pop();
			}
		}

	}

}