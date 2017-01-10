namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaString : LuaObject {

		public string value;

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

        public override string ToString(StackFrame stackFrame) {
            return ToString();
        }

        protected bool Equals(LuaString other) {
            return string.Equals(value, other.value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            } else if (ReferenceEquals(this, obj)) {
                return true;
            }else if (obj.GetType() != GetType()) {
                return false;
            } else {
                return Equals((LuaString)obj);
            }
        }

        public override int GetHashCode() {
            return (value != null ? value.GetHashCode() : 0);
        }

        public override string ToString() {
            return string.Format("\"{0}\"", value);
        }

    }

}