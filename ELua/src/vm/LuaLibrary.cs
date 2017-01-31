using System.Text;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaLibrary {

		public LVM vm;
		public StackFrame stackFrame;

		public LuaNative lenFunc;
		public LuaNative nextFunc;
		public LuaNative iterFunc;
		public LuaNative traceFunc;
		public LuaNative printFunc;
		public LuaNative pairsFunc;
		public LuaNative ipairsFunc;
		public LuaNative setmetaFunc;
		public LuaNative getmetaFunc;
		public LuaTable table;

		public LuaLibrary(LVM vm) {
			this.vm = vm;
			stackFrame = vm.stackFrame;
			Add(traceFunc = new LuaNative(vm, "trace", Trace));
			Add(printFunc = new LuaNative(vm, "print", Print));
			Add(lenFunc = new LuaNative(vm, "len", Length));
			Add(pairsFunc = new LuaNative(vm, "pairs", Pairs));
			Add(nextFunc = new LuaNative(vm, "next", Next));
			Add(ipairsFunc = new LuaNative(vm, "ipairs", IPairs));
			Add(iterFunc = new LuaNative(vm, vm.NewUID(), Iterator));
			Add(setmetaFunc = new LuaNative(vm, "setmetatable", SetMetatable));
			Add(getmetaFunc = new LuaNative(vm, "getmetatable", GetMetatable));

			Add("table", table = vm.GetTable());
			table.Bind(vm.GetString("insert"), new LuaNative(vm, "insert", Insert));
		}

		public void Add(LuaNative native) {
			stackFrame.Bind(native.name, native);
		}

		public void Add(string name, LuaTable table) {
			stackFrame.Bind(name, table);
		}

		public void Insert(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length < 2) {
				stackFrame.Push(vm.nil);
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else {
					if (args.Length == 2) {
						table.Add(args[1]);
					} else {
						var index = args[1] as LuaNumber;
						if (index == null) {
							stackFrame.Push(vm.nil);
						} else {
							table.Insert(index, args[2]);
						}
					}
				}
			}
		}

		public void SetMetatable(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length < 2) {
				stackFrame.Push(vm.nil);
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else {
					var metatable = args[1] as LuaTable;
					if (metatable == null) {
						stackFrame.Push(vm.nil);
					} else {
						table.metatable = metatable;
						stackFrame.Push(metatable);
					}
				}
			}
		}

		public void GetMetatable(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length < 1) {
				stackFrame.Push(vm.nil);
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else if (table.metatable == null) {
					stackFrame.Push(vm.nil);
				} else {
					stackFrame.Push(table.metatable);
				}
			}
		}

		public void Length(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				stackFrame.Push(vm.GetNumber(0));
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.GetNumber(0));
				} else {
					stackFrame.Push(vm.GetNumber(table.Count));
				}
			}
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			vm.WriteLine(args.FormatListString());
		}

		public void IPairs(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else if (table.status != LuaTable.Status.List) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else if (table.Count == 0) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc, args[0], vm.GetNumber(0) }));
				}
			}
		}

		public void Iterator(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length < 2) {
				stackFrame.Push(vm.nil);
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else if (table.status != LuaTable.Status.List) {
					stackFrame.Push(vm.nil);
				} else if (table.Count == 0) {
					stackFrame.Push(vm.nil);
				} else {
					var lindex = args[1] as LuaNumber;
					if (lindex == null) {
						stackFrame.Push(vm.nil);
					} else {
						lindex = vm.GetNumber(lindex.value + 1);
						if (lindex.value > table.Count) {
							stackFrame.Push(vm.nil);
						} else {
							var item = (LuaListItem)table.GetIndex(lindex);
							var value = item.value;
							if (value.Equals(vm.nil)) {
								stackFrame.Push(vm.nil);
							} else {
								stackFrame.Push(vm.GetTuple(new[] { lindex, value }));
							}
						}
					}
				}
			}
		}

		public void Pairs(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				stackFrame.Push(vm.GetTuple(new[] { nextFunc }));
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.GetTuple(new[] { nextFunc }));
				} else if (table.status == LuaTable.Status.Uninit) {
					stackFrame.Push(vm.GetTuple(new[] { nextFunc }));
				} else if (table.Count == 0) {
					stackFrame.Push(vm.GetTuple(new[] { nextFunc }));
				} else {
					stackFrame.Push(vm.GetTuple(new[] { nextFunc, args[0] }));
				}
			}
		}

		public void Next(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				stackFrame.Push(vm.nil);
			} else if (args.Length == 1) {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else if (table.status == LuaTable.Status.Uninit) {
					stackFrame.Push(vm.nil);
				} else if (table.Count == 0) {
					stackFrame.Push(vm.nil);
				} else {
					Next(stackFrame, new LuaObject[] { table, vm.nil });
				}
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.nil);
				} else if (table.status == LuaTable.Status.Uninit) {
					stackFrame.Push(vm.nil);
				} else if (table.Count == 0) {
					stackFrame.Push(vm.nil);
				} else if (Equals(args[1], vm.nil)) {
					if (table.status == LuaTable.Status.List) {
						table.iterator = table.GetEnumerator();
						var iterator = table.iterator;
						iterator.MoveNext();
						var item = (LuaListItem)iterator.Current;
						stackFrame.Push(vm.GetTuple(new[] { item.lindex, item.value }));
					} else {
						table.iterator = table.GetEnumerator();
						var iterator = table.iterator;
						iterator.MoveNext();
						var item = (LuaDictItem)iterator.Current;
						stackFrame.Push(vm.GetTuple(new[] { item.key, item.value }));
					}
				} else {
					if (table.status==LuaTable.Status.List) {
						var iterator = table.iterator;
						if (iterator == null) {
							stackFrame.Push(vm.nil);
						} else if (!iterator.MoveNext()) {
							stackFrame.Push(vm.nil);
						} else {
							var item = (LuaListItem)iterator.Current;
							stackFrame.Push(vm.GetTuple(new[] { item.lindex, item.value }));
						}
					} else {
						var iterator = table.iterator;
						if (iterator == null) {
							stackFrame.Push(vm.nil);
						} else if (!iterator.MoveNext()) {
							stackFrame.Push(vm.nil);
						} else {
							var item = (LuaDictItem)iterator.Current;
							stackFrame.Push(vm.GetTuple(new[] { item.key, item.value }));
						}
					}
				}
			}
		}

		public void Trace(StackFrame stackFrame, LuaObject[] args) {
			var sb = new StringBuilder();
			sb.Append("trace:");
			var currentFrame = stackFrame;
			while (currentFrame.module != null) {
				sb.Append(currentFrame.module.name);
				sb.Append(';');
				currentFrame = currentFrame.parent;
			}
			stackFrame.Push(vm.GetString(sb.ToString()));
		}

	}

}