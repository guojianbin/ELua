using System.Collections;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

		public bool IsInit;
		public bool IsList;
		public LuaList list;
        public LuaDict dict;
	    public IEnumerator iterator;

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

	    public LuaTable(LVM vm) : base(vm) {
			uid = vm.NewUID();
            list = new LuaList(vm, this);
            dict = new LuaDict(vm, this);
	    }

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                InitDict();
                return dict.GetProperty(obj);
            } else {
                return dict.GetProperty(obj);
            }
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            if (!IsInit) {
                InitDict();
                return dict.GetIndex(obj);
            } else if (IsList) {
                return list.GetIndex(obj);
            } else {
                return dict.GetIndex(obj);
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
	        foreach (var item in list.itemsList) {
				dict.Bind(item.lindex, item.value);
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

		public IEnumerator GetEnumerator() {
			if (!IsInit) {
				return list.GetEnumerator();
			} else if (IsList) {
				return list.GetEnumerator();
			} else {
				return dict.GetEnumerator();
			}
		}

		public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(Equals(obj));
		}

		public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return vm.GetBoolean(!Equals(obj));
		}

		public override LuaObject ToObject() {
			return this;
		}

		public override bool ToBoolean() {
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