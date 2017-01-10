using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

		public Logger logger;
		public ModuleContext context;
		public Dictionary<string, Module> modulesDict;
        public List<ByteCode> codesList;
	    public int position;
		public string name;

		public Module(Logger logger, ModuleContext context) {
			this.logger = logger;
			this.context = context;
			name = context.name;
			codesList = context.list;
			modulesDict = new Dictionary<string, Module>();
			foreach (var item in context.funcsDict) {
				modulesDict.Add(item.Key, new Module(logger, item.Value));
			}

			logger.WriteLine(string.Empty, Logger.Type.File);
			logger.WriteLine(string.Format("<{0}>", name), Logger.Type.File);
			logger.WriteLine(string.Join("\n", codesList.Select(t => t.ToString())), Logger.Type.File);
		}

		public void Jump(LuaLabel label) {
			position = label.index;
		}

		public void Return() {
			position = codesList.Count;
		}

		public LuaObject Call(StackFrame stackFrame, string name, string[] argsNames, LuaObject[] argsList) {
			return modulesDict[name].Call(stackFrame, argsNames, argsList);
		}

		public LuaObject Call(StackFrame stackFrame, string[] argsNames, LuaObject[] argsList) {
			stackFrame = new StackFrame(stackFrame) { module = this };
			foreach (var item in argsNames.Zip(argsList, (name, value) => new { name, value })) {
				stackFrame.Bind(item.name, item.value);
			}
			return Execute(stackFrame);
		}

		public LuaObject Call(StackFrame stackFrame) {
			stackFrame = new StackFrame(stackFrame) { module = this };
			return Execute(stackFrame);
		}

		private LuaObject Execute(StackFrame stackFrame) {
			for (position = 0; position < codesList.Count; position++) {
				codesList[position].Execute(stackFrame);
			}
			if (stackFrame.stackLen == 0) {
				return stackFrame.nil;
			} else {
				return stackFrame.Pop().ToObject(stackFrame);
			}
		}

	}

}
