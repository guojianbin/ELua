using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

		public string name = "main";
		public List<IL> ils;

		public void Run(StackFrame stackFrame) {
			stackFrame = new StackFrame(stackFrame);
			for (var i = 0; i < ils.Count; i++) {
				ils[i].Run(stackFrame);
			}
		}

	}

}
