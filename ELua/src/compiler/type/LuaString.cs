namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaString : LuaObject {

		public string value;

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
		    return true;
	    }

		public override string ToString() {
			return value;
		}

		public override int GetHashCode() {
			return (value != null ? value.GetHashCode() : 0);
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

    }

}