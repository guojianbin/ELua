namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public LuaDict dict;
        public LuaList list;
        public bool IsList;
        public bool IsInit;

	    public int Count {
		    get {
			    if (!IsInit) {
				    return 0;
			    } else if (IsList) {
				    return list.Count;
			    } else {
				    return dict.Count;
			    }
		    }
	    }

	    public LuaTable(LVM vm, string uid) : base(vm) {
		    this.uid = uid;
	    }

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                return vm.nil;
            } else {
                return dict.GetProperty(stackFrame, obj);
            }
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                return vm.nil;
            } else if (IsList) {
                return list.GetIndex(stackFrame, obj);
            } else {
                return dict.GetIndex(stackFrame, obj);
            }
		}

		public void InitList() {
			IsInit = true;
			IsList = true;
			if (list == null) {
				list = new LuaList(vm, this);
			} else {
				list.Clear();
			}
	    }

		public void InitDict() {
			IsInit = true;
			IsList = false;
			if (dict == null) {
				dict = new LuaDict(vm, this);
			} else {
				dict.Clear();
			}
		}

	    public void ClearList() {
		    IsList = false;
			if (list != null) {
			    list.Clear();
		    }
	    }

	    public void ClearDict() {
		    IsList = true;
			if (dict != null) {
				dict.Clear();
		    }
	    }

	    public void ClearAll() {
		    IsInit = false;
		    ClearList();
			ClearDict();
	    }

		public void Add(LuaObject value) {
			if (!IsInit) {
				InitList();
				list.Add(value);
			} else if (IsList) {
				list.Add(value);
			} else {
				ClearDict();
				InitList();
				foreach (var item in dict.itemsDict.Values) {
					list.Add(item.value);
				}
				list.Add(value);
			}
		}

		public void Bind(LuaObject key, LuaObject value) {
			if (!IsInit) {
				InitDict();
				dict.Bind(key, value);
			} else if (!IsList) {
				dict.Bind(key, value);
			} else {
				ClearList();
				InitDict();
				for (var i = 0; i < list.Count; i++) {
					dict.Bind(vm.GetNumber(i), list.IndexOf(i).value);
				}
				dict.Bind(key, value);
			}
		}

		public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
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