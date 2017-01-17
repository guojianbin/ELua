namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaString : LuaObject {

		public string value;

	    public LuaString(LVM vm, string value) : base(vm) {
		    this.value = value;
	    }

	    public override LuaObject Equal(LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override LuaObject ToObject() {
			return this;
		}

	    public override bool ToBoolean() {
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