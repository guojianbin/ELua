using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LVM : Disposable {

		public Dictionary<string, Chunk> chunksDict = new Dictionary<string, Chunk>();
		public StackFrame stackFrame = new StackFrame(null);

		public LVM() {
			stackFrame.context.Save("print", new LuaUserdata { value = new Action<StackFrame, LuaObject[]>(Print) });
		}

		public void Print(StackFrame stackFrame, LuaObject[] args) {
			Console.WriteLine(string.Join(", ", args.Select(t => t.ToString(stackFrame))));
		}

	    public void Add(Chunk chunk) {
	        chunksDict.Add(chunk.name, chunk);
	    }

	    public void Run(string name) {
			chunksDict[name].Run(stackFrame);
		}

	}

}