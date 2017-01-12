﻿using System;
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
		public LuaObject nil = new LuaNil();
		public ulong uid;
		public StackFrame stackFrame;
		public Logger logger;

        public LVM(Logger logger) {
            this.logger = logger;
			stackFrame = new StackFrame(this);
			stackFrame.Bind("trace", new LuaUserdata { uid = NewUID(), value = new Action<StackFrame, LuaObject[]>(Trace) });
			stackFrame.Bind("print", new LuaUserdata { uid = NewUID(), value = new Action<StackFrame, LuaObject[]>(Print) });
			stackFrame.Bind("len", new LuaUserdata { uid = NewUID(), value = new Action<StackFrame, LuaObject[]>(Len) });
			stackFrame.Bind("next", new LuaUserdata { uid = NewUID(), value = new Action<StackFrame, LuaObject[]>(Next) });
		}

		public string NewUID() {
			return string.Format("_<{0}>", (++uid).ToString());
		}

		public void WriteLine(string msg, Logger.Type type = Logger.Type.All) {
			logger.WriteLine(msg, type);
		}

		public void Next(StackFrame stackFrame, LuaObject[] args) {
			
		}

		public void Len(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Push(new LuaNumber { value = ((LuaTable)args[0]).Length });
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			WriteLine(string.Join(", ", args.Select(t => t.ToString())));
		}

		public void Trace(StackFrame stackFrame, LuaObject[] args) {
			var sb = new StringBuilder();
			sb.Append("trace:");
			while (stackFrame.module != null) {
				sb.Append(stackFrame.module.name);
				sb.Append(';');
				stackFrame = stackFrame.parent;
			}
			stackFrame.Push(new LuaString { value = sb.ToString() });
		}

	    public void Add(Module module) {
		    modulesDict[module.name] = module;
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