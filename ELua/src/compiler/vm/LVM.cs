using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM {

		public Dictionary<string, Module> modulesDict = new Dictionary<string, Module>();
		public StackFrame stackFrame;
        public LuaObject nil = new LuaObject { IsNil = true };

        public LVM() {
            stackFrame = new StackFrame(this);
			stackFrame.context.Bind("print", new LuaUserdata { value = new Func<StackFrame, LuaObject[], LuaObject>(Print) });
		}

		public LuaObject Print(StackFrame stackFrame, LuaObject[] args) {
			Console.WriteLine(string.Join(", ", args.Select(t => t.ToString(stackFrame))));
            return nil;
		}

	    public void Add(Module module) {
	        modulesDict.Add(module.name, module);
	    }

	    public void Run(string name) {
			modulesDict[name].Run(stackFrame);
		}

	}

}