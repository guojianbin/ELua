namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNil : LuaObject {

		public override void Unpack(StackFrame stackFrame) {
            // ignored
        }

        public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
            return new LuaBoolean { value = Equals(obj) };
        }

        public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
            return new LuaBoolean { value = !Equals(obj) };
        }

        public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override bool ToBoolean(StackFrame stackFrame) {
			return false;
		}

		public override string ToString() {
			return "nil";
        }

        public override int GetHashCode() {
            return (uid != null ? uid.GetHashCode() : 0);
        }

        protected bool Equals(LuaFunction other) {
            return string.Equals(uid, other.uid);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            } else if (ReferenceEquals(this, obj)) {
                return true;
            } else if (obj.GetType() != GetType()) {
                return false;
            } else {
                return Equals((LuaFunction)obj);
            }
        }

    }

}