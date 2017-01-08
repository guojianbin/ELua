using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaObject {

	    public bool IsNil;
	    
	    public virtual LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual void Call(StackFrame stackFrame) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual void Call(StackFrame stackFrame, LuaObject[] args) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual void Bind(StackFrame stackFrame, LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual LuaObject Negate(StackFrame stackFrame) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

        public virtual LuaObject Division(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
	        throw new InvalidOperationException(GetType().Name);
	    }

	    public virtual LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaNumber ToNumber(StackFrame stackFrame) {
            throw new InvalidOperationException(GetType().Name);
        }

		public virtual string ToString(StackFrame stackFrame) {
		    if (IsNil) {
		        return "nil";
		    } else {
                throw new InvalidOperationException(GetType().Name);
            }
		}

	}

}