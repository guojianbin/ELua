using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM {

		public Dictionary<string, Module> modulesDict = new Dictionary<string, Module>();
		public LuaObject nil = new LuaObject { IsNil = true };
		public StackFrame stackFrame;
	    public Logger logger;

        public LVM(Logger logger) {
            this.logger = logger;
            stackFrame = new StackFrame(this);
			stackFrame.Bind("print", new LuaUserdata { value = new Func<StackFrame, LuaObject[], LuaObject>(Print) });
			stackFrame.Bind("stacktrace", new LuaUserdata { value = new Func<StackFrame, LuaObject[], LuaObject>(StackTrace) });
		}

		public LuaObject Print(StackFrame stackFrame, LuaObject[] args) {
			logger.WriteLine(string.Join(", ", args.Select(t => t.ToString(stackFrame))));
            return nil;
		}

		public LuaObject StackTrace(StackFrame stackFrame, LuaObject[] args) {
			var sb = new StringBuilder();
			sb.Append("stacktrace:");
			while (stackFrame.module != null) {
				sb.Append(stackFrame.module.name);
				sb.Append('\n');
				stackFrame = stackFrame.parent;
			}
			return new LuaString { value = sb.ToString() };
		}

	    public void Add(Module module) {
	        modulesDict.Add(module.name, module);
	    }

	    public LuaObject Call(string name) {
			return modulesDict[name].Call(stackFrame);
		}

	}

}