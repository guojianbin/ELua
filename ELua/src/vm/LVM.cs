using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM {

		public Dictionary<string, Module> modulesDict = new Dictionary<string, Module>();
		public List<Module> modulesList = new List<Module>();
		public Dictionary<string, Executor> executorDict = new Dictionary<string, Executor>();
		public StackFrame stackFrame;
		public LuaPools luaPools;
		public LuaLibrary luaLibrary;
		public LuaNil nil;
		public ulong uid;
		public Logger logger;

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
		    module.index = modulesList.Count;
			modulesList.Add(module);
	    }

		public Module GetModule(int index) {
			return modulesList[index];
		}

		public LuaObject Call(int index) {
			return Call(GetModule(index));			
		}

	    public LuaObject Call(string name) {
			return Call(modulesDict[name]);
	    }

		public LuaObject Call(Module module) {
			module.Call(stackFrame);
			return stackFrame.PopResult();
		}

		public void Execute(Executor executor) {
			executorDict.Add(executor.uid, executor);
			executor.Start();
		}

		public void Remove(Executor executor) {
			executorDict.Remove(executor.uid);
		}

	}

}