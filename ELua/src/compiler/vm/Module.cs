using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Module {

        public List<ByteCode> byteCodes;
	    public int position;
        public string name;

        public void Run(StackFrame stackFrame) {
            stackFrame = new StackFrame(stackFrame) { module = this };
            for (position = 0; position < byteCodes.Count; position++) {
				byteCodes[position].Run(stackFrame);
			}
		}

	    public void Jump(LuaObject label) {
	        for (position += 1; position < byteCodes.Count; position++) {
	            var il = byteCodes[position];
	            if (il.opCode == ByteCode.OpCode.Label && il.opArg.Equals(label) ) {
	                break;
	            }
	        }
	    }

	}

}
