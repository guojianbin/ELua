using System;
using System.Linq;
using System.Text;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaLibrary {

		public LVM vm;
		public StackFrame stackFrame;

		public LuaLibrary(LVM vm) {
			this.vm = vm;
			stackFrame = vm.stackFrame;
			stackFrame.Bind("trace", vm.GetUserdata(new Action<StackFrame, LuaObject[]>(Trace)));
			stackFrame.Bind("print", vm.GetUserdata(new Action<StackFrame, LuaObject[]>(Print)));
			stackFrame.Bind("len", vm.GetUserdata(new Action<StackFrame, LuaObject[]>(Len)));
		}

		public void Len(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Push(vm.GetNumber(((LuaTable)args[0]).Count));
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			vm.WriteLine(string.Join(", ", args.Select(t => t.ToString())));
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