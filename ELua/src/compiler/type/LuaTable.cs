namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public LuaDict dict;
        public LuaList list;
        public bool IsList;
        public bool IsInit;

	    public int Length {
		    get {
			    if (!IsInit) {
				    return 0;
			    } else if (IsList) {
				    return list.Length;
			    } else {
				    return dict.Length;
			    }
		    }
	    }

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                return stackFrame.nil;
            } else {
                return dict.GetProperty(stackFrame, obj);
            }
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                return stackFrame.nil;
            } else if (IsList) {
                return list.GetIndex(stackFrame, obj);
            } else {
                return dict.GetIndex(stackFrame, obj);
            }
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
			return true;
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

		public override string ToString() {
			if (!IsInit) {
				return "{ }";
			} else if (IsList) {
				return list.ToString();
			} else {
				return dict.ToString();
			}
		}

    }

}