using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

		public LVM vm;
		private Logger logger;
		public Module parent;
		public ModuleContext context;
		public Dictionary<string, Module> modulesDict;
        public List<ByteCode> codesList;
		public string name;

		public Module(LVM vm, Logger logger, ModuleContext context) {
			this.vm = vm;
			this.logger = logger;
			this.context = context;
			name = context.name;
			codesList = context.list;
			modulesDict = new Dictionary<string, Module>();
			InitChildren();

			logger.WriteLine(string.Empty, Logger.Type.File);
			logger.WriteLine(string.Format("<{0}>", name), Logger.Type.File);
			logger.WriteLine(string.Join("\n", codesList.Select(t => t.ToString())), Logger.Type.File);
		}

		public void InitChildren() {
			var childList = new List<Module>(context.childDict.Count);
			foreach (var item in context.childDict) {
				var module = new Module(vm, logger, item.Value) { parent = this };
				modulesDict.Add(item.Key, module);
				childList.Add(module);
			}
			foreach (var item in childList.SelectMany(t => ModuleHelper.GetChildren(t))) {
				modulesDict.Add(item.name, item);
			}
		}

		public Module Find(string name) {
	        Module value;
	        if (modulesDict.TryGetValue(name, out value)) {
	            return value;
	        } else {
	            value = FindParent(name);
				modulesDict.Add(name, value);
				return value;
	        }
	    }

	    private Module FindParent(string name) {
	        if (parent == null) {
	            return null;
	        } else {
	            return parent.Find(name);
	        }
	    }

		public void Call(StackFrame stackFrame, string name, string[] argsNames, LuaObject[] argsList) {
		    var module = Find(name);
			module.Call(stackFrame, argsNames, argsList);
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
