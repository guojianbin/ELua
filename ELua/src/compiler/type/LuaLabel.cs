namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaLabel : LuaObject {

        public string value;

        public override string ToString(StackFrame stackFrame) {
            return ToString();
        }

        public override string ToString() {
            return string.Format("{0}:", value);
        }

        protected bool Equals(LuaLabel other) {
            return string.Equals(value, other.value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            } else if (ReferenceEquals(this, obj)) {
                return true;
            } else if (obj.GetType() != GetType()) {
                return false;
            } else {
                return Equals((LuaLabel)obj);
            }
        }

        public override int GetHashCode() {
            return (value != null ? value.GetHashCode() : 0);
        }

    }

}