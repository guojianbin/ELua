namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaModule : LuaObject {

        public Module value;

        public LuaModule(LVM vm, Module value) : base(vm, Type.Module) {
            this.value = value;
            uid = vm.NewUID();
        }

        public override string ToString() {
            return value.ToString();
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