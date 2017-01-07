using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaObject {

        public virtual void Call(StackFrame stackFrame) {
            throw new NotImplementedException();
        }

        public virtual void Call(StackFrame stackFrame, LuaObject[] args) {
            throw new NotImplementedException();
        }

        public virtual LuaObject Multiply(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

        public virtual LuaObject Division(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

        public virtual LuaObject Mod(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

        public virtual LuaObject Plus(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

        public virtual LuaObject Subtract(LuaObject obj) {
			throw new InvalidOperationException(GetType().Name);
		}

		public virtual string ToString(StackFrame stackFrame) {
			throw new InvalidOperationException(GetType().Name);
		}

	}

}