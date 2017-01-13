using System;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

		public LVM vm;
		public Module parent;
		public ModuleContext context;
        public List<ByteCode> codesList;
		public string name;
		public int level;

		public Module(ModuleContext context) {
			this.context = context;
			vm = context.vm;
			name = context.name;
			codesList = context.list;
			level = context.level;
		}

		public void Call(StackFrame stackFrame, string[] argsNames, LuaObject[] argsList) {
			var executor = stackFrame.executor;
			stackFrame = new StackFrame(stackFrame, this, vm.NewUID());
			var len = Math.Min(argsNames.Length, argsList.Length);
			for (var i = 0; i < len; i++) {
				stackFrame.Bind(argsNames[i], argsList[i]);
			}
			executor.Execute(stackFrame);
		}

		public void Call(StackFrame stackFrame) {
			stackFrame = new StackFrame(stackFrame, this, vm.NewUID());
			var executor = new Executor(stackFrame);
			stackFrame.executor = executor;
			vm.Execute(executor);
		}

		public override string ToString() {
			return name;
		}

	}

}
