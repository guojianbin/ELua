namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaFunction : LuaObject {

	    public static readonly string[] emptyList = new string[0];
        public string[] argsList = emptyList;
        public string name;

		public override LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
		    var newArgs = new LuaObject[args.Length];
		    for (var i = 0; i < args.Length; i++) {
                newArgs[i] = args[i].ToObject(stackFrame);
		    }
			return stackFrame.module.Call(stackFrame, name, argsList, newArgs);
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override string ToString() {
			return string.Format("function {0}", name);
		}

	}

}
