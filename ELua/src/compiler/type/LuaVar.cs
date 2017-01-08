using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject {

		public string value;

		public override string ToString(StackFrame stackFrame) {
			return stackFrame.Find(value).ToString(stackFrame);
		}

	    public override void Call(StackFrame stackFrame) {
	        throw new NotImplementedException();
	    }

	    public override void Call(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Find(value).Call(stackFrame, args);
		}

	    public override LuaObject Negate(StackFrame stackFrame) {
            return stackFrame.Find(value).Negate(stackFrame);
        }

	    public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(value).Multiply(stackFrame, obj);
        }

	    public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(value).Division(stackFrame, obj);
        }

	    public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(value).Mod(stackFrame, obj);
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(value).Plus(stackFrame, obj);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(value).Subtract(stackFrame, obj);
        }

	    public override LuaNumber ToNumber(StackFrame stackFrame) {
	        return (LuaNumber)stackFrame.Find(value);
	    }

	    public override string ToString() {
			return value;
		}

	}

}