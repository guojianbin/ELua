using System;
using System.Collections;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

	    /// <summary>
	    /// @author Easily
	    /// </summary>
	    public enum Status : byte {

		    Uninit, 
			List, 
			Dict,

	    }

	    public Status status = Status.Uninit;
		public LuaList list;
		public LuaDict dict;
		public IEnumerator iterator;
		public LuaTable metatable;

	    public int Count {
		    get {
			    switch (status) {
				    case Status.Uninit:
					    return 0;
				    case Status.List:
					    return list.Count;
				    case Status.Dict:
					    return dict.Count;
				    default:
					    throw new ArgumentOutOfRangeException(ToString());
			    }
		    }
	    }

	    public LuaTable(LVM vm) : base(vm, Type.Table) {
			uid = vm.NewUID();
            list = new LuaList(vm, this);
            dict = new LuaDict(vm, this);
	    }

	    public override LuaObject GetProperty(LuaObject obj) {
		    switch (status) {
				case Status.Uninit:
					InitDict();
					return dict.GetProperty(obj);
				case Status.List:
					List2Dict();
					return dict.GetProperty(obj);
				case Status.Dict:
					return dict.GetProperty(obj);
			    default:
				    throw new ArgumentOutOfRangeException(ToString());
		    }
        }

        public override LuaObject GetIndex(LuaObject obj) {
	        switch (status) {
				case Status.Uninit:
					InitDict();
					return dict.GetIndex(obj);
				case Status.List:
					return list.GetIndex(obj);
				case Status.Dict:
					return dict.GetIndex(obj);
		        default:
			        throw new ArgumentOutOfRangeException(ToString());
	        }
		}

		public void InitList() {
			status = Status.List;
			list.Clear();
	    }

		public void InitDict() {
			status = Status.Dict;
			dict.Clear();
		}

	    public void ClearList() {
			list.Clear();
	    }

	    public void ClearDict() {
			dict.Clear();
	    }

		public void ClearAll() {
			status = Status.Uninit;
			metatable = null;
		    ClearList();
			ClearDict();
		}

		public void Add(LuaObject value) {
			switch (status) {
				case Status.Uninit:
					InitList();
					list.Add(value);
					break;
				case Status.List:
					list.Add(value);
					break;
				case Status.Dict:
					Dict2List();
					list.Add(value);
					break;
				default:
					throw new ArgumentOutOfRangeException(ToString());
			}
		}

		public void Insert(LuaObject index, LuaObject value) {
		    switch (status) {
			    case Status.Uninit:
					InitList();
					list.Insert(index, value);
				    break;
			    case Status.List:
					list.Insert(index, value);
				    break;
			    case Status.Dict:
					Dict2List();
					list.Insert(index, value);
				    break;
			    default:
				    throw new ArgumentOutOfRangeException();
		    }
	    }

		public void Bind(LuaObject key, LuaObject value) {
			switch (status) {
				case Status.Uninit:
					InitDict();
					dict.Bind(key, value);
					break;
				case Status.List:
					List2Dict();
					dict.Bind(key, value);
					break;
				case Status.Dict:
					dict.Bind(key, value);
					break;
				default:
					throw new ArgumentOutOfRangeException(ToString());
			}
		}

	    public override LuaObject Add(StackFrame stackFrame, LuaObject obj) {
		    if (metatable == null) {
			    return vm.nil;
			} else if (metatable.status != Status.Dict) {
				return vm.nil;
			} else if (metatable.Count == 0) {
				return vm.nil;
			} else {
			    var table = obj as LuaTable;
			    if (table == null) {
				    return vm.nil;
			    } else {
				    var add = (LuaDictItem)metatable.GetProperty(vm.GetString("__add"));
				    if (Equals(add.value, vm.nil)) {
					    return vm.nil;
				    } else {
					    var func = add.value as LuaFunction;
					    if (func == null) {
						    return vm.nil;
					    } else {
							func.Call(stackFrame, new LuaObject[] { this, table });
						    return stackFrame.PopResult();
					    }
				    }
			    }
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
			switch (status) {
				case Status.Uninit:
					return list.GetEnumerator();
				case Status.List:
					return list.GetEnumerator();
				case Status.Dict:
					return dict.GetEnumerator();
				default:
					throw new ArgumentOutOfRangeException(ToString());
			}
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
			switch (status) {
				case Status.Uninit:
					return "{ }";
				case Status.List:
					return list.ToString();
				case Status.Dict:
					return dict.ToString();
				default:
					throw new ArgumentOutOfRangeException(ToString());
			}
		}

    }

}