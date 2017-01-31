using System;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

		public LVM vm;
		public int index;
		public Module parent;
		public ModuleContext context;
        public List<ByteCode> codesList;
		public string name;
	    public string uid;
		public int level;

		public Module(ModuleContext context) {
			this.context = context;
			vm = context.vm;
		    uid = vm.NewUID();
			name = context.name;
			codesList = context.list;
			level = context.level;
		}

		public void Call(StackFrame parent) {
			var stackFrame = vm.GetStackFrame(this, parent);
			var executor = new Executor(stackFrame);
			stackFrame.executor = executor;
			vm.Execute(executor);
		}

		public void Call(StackFrame parent, StackFrame upvalue, string[] argsNames, LuaObject[] argsList) {
			var executor = parent.executor;
			var stackFrame = vm.GetStackFrame(this, parent, upvalue);
			var len = Math.Min(argsNames.Length, argsList.Length);
			for (var i = 0; i < len; i++) {
				stackFrame.Bind(argsNames[i], argsList[i]);
			}
			executor.Execute(stackFrame);
		}

		public override string ToString() {
			return name;
		}

	}

}
