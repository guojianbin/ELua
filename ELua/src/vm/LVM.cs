using System;
using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM : IDisposable {

		public Dictionary<string, Module> modulesDict = new Dictionary<string, Module>();
		public Dictionary<string, Executor> executorDict = new Dictionary<string, Executor>();
		public StackFrame stackFrame;
		public LuaPools luaPools;
		public LuaLibrary luaLibrary;
		public LuaNil nil;
		public ulong uid;
		public Logger logger;
	    public bool IsDisposed;

        public LVM(Logger logger) {
            this.logger = logger;
            nil = new LuaNil(this, NewUID());
            luaPools = new LuaPools(this);
            stackFrame = new StackFrame(this);
			luaLibrary = new LuaLibrary(this);
		}

		public string NewUID() {
			return string.Format("#{0}", (++uid).ToString());
		}

		public void WriteLine(string msg, Logger.Type type = Logger.Type.All) {
			logger.WriteLine(msg, type);
		}

	    public void Add(Module module) {
		    modulesDict[module.name] = module;
	    }

	    public LuaObject Call(string name) {
			modulesDict[name].Call(stackFrame);
	        return stackFrame.PopResult();
	    }

		public void Execute(Executor executor) {
			executorDict.Add(executor.uid, executor);
			executor.Start();
		}

		public void Remove(Executor executor) {
			executorDict.Remove(executor.uid);
		}

	    public void Dispose() {
	        IsDisposed = true;
	    }

	}

}