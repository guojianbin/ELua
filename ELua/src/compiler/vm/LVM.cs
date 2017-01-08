using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM : Disposable {

		public Dictionary<string, Module> modulesDict = new Dictionary<string, Module>();
		public StackFrame stackFrame;

		public LVM() {
            stackFrame = new StackFrame(this);
			stackFrame.context.Bind("print", new LuaUserdata { value = new Action<StackFrame, LuaObject[]>(Print) });
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			Console.WriteLine(string.Join(", ", args.Select(t => t.ToString(stackFrame))));
		}

	    public void Add(Module module) {
	        modulesDict.Add(module.name, module);
	    }

	    public void Run(string name) {
			modulesDict[name].Run(stackFrame);
		}

	}

}