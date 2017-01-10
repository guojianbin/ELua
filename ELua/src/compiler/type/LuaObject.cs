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
	    
	    public virtual LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual LuaObject Call(StackFrame stackFrame, LuaObject[] args) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual LuaObject Bind(StackFrame stackFrame, LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public LuaObject Not(StackFrame stackFrame) {
            return new LuaBoolean { value = !ToBoolean(stackFrame) };
        }

        public LuaObject And(StackFrame stackFrame, LuaObject obj) {
	        return ToBoolean(stackFrame) ? obj : this;
        }

		public LuaObject Or(StackFrame stackFrame, LuaObject obj) {
			return ToBoolean(stackFrame) ? this : obj;
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

	    public virtual LuaObject Less(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject Greater(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject LessEqual(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject GreaterEqual(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaNumber ToNumber(StackFrame stackFrame) {
            throw new InvalidOperationException(GetType().Name);
        }

		public virtual LuaObject ToObject(StackFrame stackFrame) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual bool ToBoolean(StackFrame stackFrame) {
	        if (IsNil) {
	            return false;
	        } else {
	            throw new InvalidOperationException(GetType().Name);
	        }
        }

		public virtual string ToString(StackFrame stackFrame) {
		    if (IsNil) {
		        return "nil";
		    } else {
                throw new InvalidOperationException(GetType().Name);
            }
		}

		public override string ToString() {
			if (IsNil) {
				return "nil";
			} else {
				throw new InvalidOperationException(GetType().Name);
			}
		}

	}

}