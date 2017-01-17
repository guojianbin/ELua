namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaNil : LuaObject {

		public LuaNil(LVM vm, string uid) : base(vm) {
			this.uid = uid;
		}

		public override void Unpack(StackFrame stackFrame) {
            // ignored
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