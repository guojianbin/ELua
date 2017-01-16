using System.Linq;
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

		public LuaLibrary(LVM vm) {
			this.vm = vm;
			stackFrame = vm.stackFrame;
			Add(traceFunc = new LuaNative(vm, "trace", Trace));
			Add(printFunc = new LuaNative(vm, "print", Print));
			Add(lenFunc = new LuaNative(vm, "len", Len));
			Add(pairsFunc = new LuaNative(vm, "pairs", Pairs));
			Add(nextFunc = new LuaNative(vm, "next", Next));
			Add(ipairsFunc = new LuaNative(vm, "ipairs", IPairs));
			Add(iterFunc = new LuaNative(vm, "#iter", Iterator));
		}

		public void Add(LuaNative native) {
			stackFrame.Bind(native.name, native);
		}

		public void Len(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Push(vm.GetNumber(((LuaTable)args[0]).Count));
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			vm.WriteLine(string.Join(", ", args.Select(t => t.ToString())));
		}

		public void IPairs(StackFrame stackFrame, LuaObject[] args) {
			if (args.Length == 0) {
				stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
			} else {
				var table = args[0] as LuaTable;
				if (table == null) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else if (!table.IsInit) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else if (table.Count == 0) {
					stackFrame.Push(vm.GetTuple(new[] { iterFunc }));
				} else if (!table.IsList) {
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
				} else if (!table.IsInit) {
					stackFrame.Push(vm.nil);
				} else if (table.Count == 0) {
					stackFrame.Push(vm.nil);
				} else if (!table.IsList) {
					stackFrame.Push(vm.nil);
				} else {
					var index = args[1] as LuaNumber;
					if (index == null) {
						stackFrame.Push(vm.nil);
					} else {
						index = vm.GetNumber(index.value + 1);
						var item = (LuaListItem)table.GetIndex(index);
						var value = item.value;
						if (value.Equals(vm.nil)) {
							stackFrame.Push(vm.nil);
						} else {
							stackFrame.Push(vm.GetTuple(new[] { index, value }));
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
				} else if (!table.IsInit) {
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
				} else if (!table.IsInit) {
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
				} else if (!table.IsInit) {
					stackFrame.Push(vm.nil);
				} else if (table.Count == 0) {
					stackFrame.Push(vm.nil);
				} else if (Equals(args[1], vm.nil)) {
					if (table.IsList) {
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
					if (table.IsList) {
						var iterator = table.iterator;
						if (!iterator.MoveNext()) {
							stackFrame.Push(vm.nil);
						} else {
							var item = (LuaListItem)iterator.Current;
							stackFrame.Push(vm.GetTuple(new[] { item.lindex, item.value }));
						}
					} else {
						var iterator = table.iterator;
						if (!iterator.MoveNext()) {
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