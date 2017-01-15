namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaListItem : LuaObject {

        public LuaTable table;
        public LuaList list;
        public int index;
        public LuaObject value;

	    public LuaListItem(LVM vm, LuaTable table, LuaList list, int index, LuaObject value) : base(vm) {
		    this.table = table;
		    this.list = list;
		    this.index = index;
		    this.value = value;
	    }

	    public override void Bind(StackFrame stackFrame, LuaObject obj) {
			list.Bind(index, obj);
        }

        public override LuaObject Negate(StackFrame stackFrame) {
            return value.Negate(stackFrame);
        }

        public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
            return value.Multiply(stackFrame, obj);
        }

        public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
            return value.Division(stackFrame, obj);
        }

        public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
            return value.Mod(stackFrame, obj);
        }

        public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
            return value.Plus(stackFrame, obj);
        }

        public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
            return value.Subtract(stackFrame, obj);
        }

		public override LuaObject ToObject(StackFrame stackFrame) {
			return value;
		}

	    public override string ToString() {
		    return value.ToString();
	    }

	    public override bool ToBoolean(StackFrame stackFrame) {
			return value.ToBoolean(stackFrame);
	    }

	    public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return value.NotEqual(stackFrame, obj);
	    }

	    public override LuaObject LessEqual(StackFrame stackFrame, LuaObject obj) {
			return value.LessEqual(stackFrame, obj);
	    }

	    public override LuaObject Less(StackFrame stackFrame, LuaObject obj) {
			return value.Less(stackFrame, obj);
	    }

	    public override LuaObject GreaterEqual(StackFrame stackFrame, LuaObject obj) {
			return value.GreaterEqual(stackFrame, obj);
	    }

	    public override LuaObject Greater(StackFrame stackFrame, LuaObject obj) {
			return value.Greater(stackFrame, obj);
	    }

	    public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
			return value.GetProperty(stackFrame, obj);
	    }

	    public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
			return value.GetIndex(stackFrame, obj);
	    }

	    public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return value.Equal(stackFrame, obj);
	    }

	    public override void Call(StackFrame stackFrame, LuaObject[] args) {
			value.Call(stackFrame, args);
		}

        public override LuaNumber ToNumber(StackFrame stackFrame) {
            return value.ToNumber(stackFrame);
        }

    }

}