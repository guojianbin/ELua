﻿using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaVar : LuaObject {

		public string name;

		public override string ToString(StackFrame stackFrame) {
			return stackFrame.Find(name).ToString(stackFrame);
		}

	    public override void Call(StackFrame stackFrame) {
	        throw new NotImplementedException();
	    }

	    public override void Call(StackFrame stackFrame, LuaObject[] args) {
			stackFrame.Find(name).Call(stackFrame, args);
		}

	    public override LuaObject Negate(StackFrame stackFrame) {
            return stackFrame.Find(name).Negate(stackFrame);
        }

	    public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Multiply(stackFrame, obj);
        }

	    public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Division(stackFrame, obj);
        }

	    public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Mod(stackFrame, obj);
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Plus(stackFrame, obj);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return stackFrame.Find(name).Subtract(stackFrame, obj);
        }

	    public override LuaNumber ToNumber(StackFrame stackFrame) {
	        return (LuaNumber)stackFrame.Find(name);
	    }

	    public override string ToString() {
			return name;
		}

	}

}