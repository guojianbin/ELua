using System;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaNumber : LuaObject {

        public float value;

	    public LuaNumber(LVM vm, float value) : base(vm, Type.Number) {
		    this.value = value;
	    }

	    public override LuaObject Negate(StackFrame stackFrame) {
	        return vm.GetNumber(-value);
        }

        public override LuaObject Power(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber((float)Math.Pow(value, obj.ToNumber().value));
        }

        public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber(value * obj.ToNumber().value);
        }

        public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber(value / obj.ToNumber().value);
        }

        public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber(value % obj.ToNumber().value);
        }

        public override LuaObject Add(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber(value + obj.ToNumber().value);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return vm.GetNumber(value - obj.ToNumber().value);
        }

        public override LuaObject Less(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(value < obj.ToNumber().value);
        }

        public override LuaObject Greater(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(value > obj.ToNumber().value);
        }

        public override LuaObject LessEqual(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(value <= obj.ToNumber().value);
        }

        public override LuaObject GreaterEqual(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(value >= obj.ToNumber().value);
		}

		public override LuaObject Equal(LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

        public override LuaNumber ToNumber() {
            return this;
        }

        public override bool ToBoolean() {
            return true;
        }

		public override LuaObject ToObject() {
			return this;
		}

        public override string ToString() {
            return value.ToString();
        }

		public override int GetHashCode() {
			return value.GetHashCode();
		}

        protected bool Equals(LuaNumber other) {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            } else if (ReferenceEquals(this, obj)) {
                return true;
            } else if (obj.GetType() != GetType()) {
                return false;
            } else {
                return Equals((LuaNumber)obj);
            }
        }

    }

}