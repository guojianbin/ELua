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
            list = new LuaList(vm, this);
            dict = new LuaDict(vm, this);
        }

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                InitDict();
                return dict.GetProperty(stackFrame, obj);
            } else {
                return dict.GetProperty(stackFrame, obj);
            }
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                InitDict();
                return dict.GetIndex(stackFrame, obj);
            } else if (IsList) {
                return list.GetIndex(stackFrame, obj);
            } else {
                return dict.GetIndex(stackFrame, obj);
            }
		}

		public void InitList() {
			IsInit = true;
			IsList = true;
			list.Clear();
	    }

		public void InitDict() {
			IsInit = true;
			IsList = false;
			dict.Clear();
		}

	    public void ClearList() {
		    IsList = false;
			list.Clear();
	    }

	    public void ClearDict() {
		    IsList = true;
			dict.Clear();
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
                Dict2List();
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
                List2Dict();
				dict.Bind(key, value);
			}
		}

        public void List2Dict() {
            InitDict();
            for (var i = 0; i < list.Count; i++) {
                dict.Bind(vm.GetNumber(i), list.IndexOf(i).value);
            }
            ClearList();
        }

        public void Dict2List() {
            InitList();
            foreach (var item in dict.itemsDict.Values) {
                list.Add(item.value);
            }
            ClearDict();
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