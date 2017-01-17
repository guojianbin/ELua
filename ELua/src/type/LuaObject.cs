using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public abstract class LuaObject {

	    public LVM vm;
		public string uid;

	    protected LuaObject(LVM vm) {
			this.vm = vm;
		}

		public virtual void Unpack(StackFrame stackFrame) {
			stackFrame.Push(this);
		}

		public virtual LuaObject GetProperty(LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }
	    
	    public virtual LuaObject GetIndex(LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual void Call(StackFrame stackFrame, LuaObject[] args) {
            throw new InvalidOperationException(GetType().Name);
        }

        public virtual void Bind(LuaObject obj) {
            throw new InvalidOperationException(GetType().Name);
        }

        public LuaObject Not() {
			return vm.GetBoolean(!ToBoolean());
        }

        public LuaObject And(LuaObject obj) {
	        return ToBoolean() ? obj : this;
        }

		public LuaObject Or(LuaObject obj) {
			return ToBoolean() ? this : obj;
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

	    public virtual LuaObject Equal(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaObject NotEqual(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

	    public virtual LuaNumber ToNumber() {
            throw new InvalidOperationException(GetType().Name);
        }

		public virtual LuaObject ToObject() {
			throw new InvalidOperationException(GetType().Name);
		}

		public virtual bool ToBoolean() {
			throw new InvalidOperationException(GetType().Name);
        }

		public override string ToString() {
			throw new InvalidOperationException(GetType().Name);
		}

	}

}