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
		public Dictionary<string, Executor> executorDict = new Dictionary<string, Executor>();
		public LuaObject nil = new LuaObject { IsNil = true };
		public ulong uid;
		public StackFrame stackFrame;
		public Logger logger;

		public Stack<StackFrame> callStack = new Stack<StackFrame>();
		public StackFrame currentFrame;
		public bool IsExecuting;

        public LVM(Logger logger) {
            this.logger = logger;
            stackFrame = new StackFrame(this);
			stackFrame.Bind("print", new LuaUserdata { uid = NewUID(), value = new Func<StackFrame, LuaObject[], LuaObject>(Print) });
			stackFrame.Bind("stacktrace", new LuaUserdata { uid = NewUID(), value = new Func<StackFrame, LuaObject[], LuaObject>(StackTrace) });
		}

		public string NewUID() {
			return string.Format("_u_<{0}>", (++uid).ToString());
		}

		public LuaObject Print(StackFrame stackFrame, LuaObject[] args) {
			logger.WriteLine(string.Join(", ", args.Select(t => t.ToString())));
            return nil;
		}

		public LuaObject StackTrace(StackFrame stackFrame, LuaObject[] args) {
			var sb = new StringBuilder();
			sb.Append("stacktrace:");
			while (stackFrame.module != null) {
				sb.Append(stackFrame.module.name);
				sb.Append(';');
				stackFrame = stackFrame.parent;
			}
			return new LuaString { value = sb.ToString() };
		}

		public void Generate(ModuleContext context) {
			Add(new Module(this, logger, context));
		}

	    public void Add(Module module) {
	        modulesDict.Add(module.name, module);
	    }

	    public void Call(string name) {
			modulesDict[name].Call(stackFrame);
		}

		public void Execute(Executor executor) {
			executorDict.Add(executor.uid, executor);
			executor.Start();
		}

		public void Remove(Executor executor) {
			executorDict.Remove(executor.uid);
			stackFrame.Clear();
		}

	}

}