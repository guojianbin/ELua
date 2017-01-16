using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaPools {

        public LVM vm;
        public Queue<LuaNumber> numberPool = new Queue<LuaNumber>();
		public Queue<LuaBoolean> boolPool = new Queue<LuaBoolean>();
		public Queue<LuaString> stringPool = new Queue<LuaString>();
		public Queue<LuaTable> tablePool = new Queue<LuaTable>();
		public Queue<LuaUserdata> userdataPool = new Queue<LuaUserdata>();
		public Queue<LuaTuple> tuplePool = new Queue<LuaTuple>();
		public Queue<LuaVar> varPool = new Queue<LuaVar>();
		public Queue<LuaFunction> functionPool = new Queue<LuaFunction>();
		public Queue<LuaListItem> listItemPool = new Queue<LuaListItem>();
		public Queue<LuaDictItem> dictItemPool = new Queue<LuaDictItem>();

        public LuaPools(LVM vm) {
            this.vm = vm;
        }

        public LuaNumber GetNumber(float value) {
			if (numberPool.Count == 0) {
				return new LuaNumber(vm, value);
            } else {
                var item = numberPool.Dequeue();
                item.value = value;
                return item;
            }
        }

        public void PutNumber(LuaNumber item) {
            numberPool.Enqueue(item);
        }

        public LuaBoolean GetBoolean(bool value) {
			if (boolPool.Count == 0) {
				return new LuaBoolean(vm, value);
            } else {
                var item = boolPool.Dequeue();
                item.value = value;
                return item;
            }
        }

        public void PutBoolean(LuaBoolean item) {
            boolPool.Enqueue(item);
		}

		public LuaString GetString(string value) {
			if (stringPool.Count == 0) {
				return new LuaString(vm, value);
			} else {
				var item = stringPool.Dequeue();
				item.value = value;
				return item;
			}
		}

		public void PutString(LuaString item) {
			stringPool.Enqueue(item);
        }

		public LuaFunction GetFunction(Module module, StackFrame stackFrame) {
            if (functionPool.Count == 0) {
                return new LuaFunction(vm, module, stackFrame);
            } else {
                var item = functionPool.Dequeue();
                item.module = module;
                item.stackFrame = stackFrame;
	            item.uid = vm.NewUID();
                return item;
            }
        }

        public void PutFunction(LuaFunction item) {
            functionPool.Enqueue(item);
        }

		public LuaVar GetVar(string name, LuaBinder binder) {
			if (varPool.Count == 0) {
				return new LuaVar(vm, name, binder);
			} else {
				var item = varPool.Dequeue();
				item.name = name;
				item.binder = binder;
				item.target = binder.target;
				return item;
			}
		}

		public void PutVar(LuaVar item) {
			varPool.Enqueue(item);
		}

		public LuaTable GetTable() {
			if (tablePool.Count == 0) {
				return new LuaTable(vm, vm.NewUID());
			} else {
				var item = tablePool.Dequeue();
				return item;
			}
		}

		public void PutTable(LuaTable item) {
			item.ClearAll();
			tablePool.Enqueue(item);
		}

		public LuaUserdata GetUserdata(object value) {
			if (userdataPool.Count == 0) {
				return new LuaUserdata(vm, value);
			} else {
				var item = userdataPool.Dequeue();
				item.value = value;
				item.uid = vm.NewUID();
				return item;
			}
		}

		public void PutUserdata(LuaUserdata item) {
			userdataPool.Enqueue(item);
		}

		public LuaTuple GetTuple(IEnumerable<LuaObject> list) {
			if (tuplePool.Count == 0) {
				return new LuaTuple(vm, list);
			} else {
				var item = tuplePool.Dequeue();
				item.uid = vm.NewUID();
				item.AddRange(list);
				return item;
			}
		}

		public void PutTuple(LuaTuple item) {
			item.Clear();
			tuplePool.Enqueue(item);
		}

		public LuaListItem GetListItem(LuaTable table, LuaList list, int index, LuaObject value) {
			if (listItemPool.Count == 0) {
				return new LuaListItem(vm, table, list, index, value);
			} else {
				var item = listItemPool.Dequeue();
				item.table = table;
				item.list = list;
				item.index = index;
				item.value = value;
				item.lindex = vm.GetNumber(index + 1);
				return item;
			}
		}

		public void PutListItem(LuaListItem item) {
			listItemPool.Enqueue(item);
		}

		public LuaDictItem GetDictItem(LuaTable table, LuaDict dict, LuaObject key, LuaObject value) {
			if (dictItemPool.Count == 0) {
				return new LuaDictItem(vm, table, dict, key, value);
			} else {
				var item = dictItemPool.Dequeue();
				item.table = table;
				item.dict = dict;
				item.key = key;
				item.value = value;
				return item;
			}
		}

		public void PutDictItem(LuaDictItem item) {
			dictItemPool.Enqueue(item);
		}

    }

}