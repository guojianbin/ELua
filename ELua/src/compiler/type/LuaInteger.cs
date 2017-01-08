namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaInteger : LuaObject {

        public float value;

        public override LuaObject Negate(StackFrame stackFrame) {
            return new LuaInteger { value = -value };
        }

        public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return new LuaInteger { value = value * obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return new LuaInteger { value = value / obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return new LuaInteger { value = value % obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return new LuaInteger { value = value + obj.ToNumber(stackFrame).value };
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return new LuaInteger { value = value - obj.ToNumber(stackFrame).value };
        }

        public override string ToString(StackFrame stackFrame) {
            return value.ToString();
        }

        public override string ToString() {
            return value.ToString();
        }

        protected bool Equals(LuaInteger other) {
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
                return Equals((LuaInteger)obj);
            }
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

    }

}