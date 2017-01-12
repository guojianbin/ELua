using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaUserdata : LuaObject {

		public object value;

		public override LuaObject Division(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Division(stackFrame, obj);
		}

		public override LuaObject Greater(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Greater(stackFrame, obj);
		}

		public override LuaObject GreaterEqual(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).GreaterEqual(stackFrame, obj);
		}

		public override LuaObject Less(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Less(stackFrame, obj);
		}

		public override LuaObject LessEqual(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).LessEqual(stackFrame, obj);
		}

		public override LuaObject Mod(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Mod(stackFrame, obj);
		}

		public override LuaObject Multiply(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Multiply(stackFrame, obj);
		}

		public override LuaObject Negate(StackFrame stackFrame) {
			return ToNumber(stackFrame).Negate(stackFrame);
		}

		public override LuaObject Plus(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Plus(stackFrame, obj);
		}

		public override LuaObject Subtract(StackFrame stackFrame, LuaObject obj) {
			return ToNumber(stackFrame).Subtract(stackFrame, obj);
		}

		public override bool ToBoolean(StackFrame stackFrame) {
			return Convert.ToBoolean(value);
		}

		public override LuaObject ToObject(StackFrame stackFrame) {
			return this;
		}

		public override void Call(StackFrame stackFrame, LuaObject[] args) {
			((Action<StackFrame, LuaObject[]>)value)(stackFrame, args);
		}

		public override LuaObject Equal(StackFrame stackFrame, LuaObject obj) {
			return new LuaBoolean { value = Equals(obj) };
		}

		public override LuaObject NotEqual(StackFrame stackFrame, LuaObject obj) {
			return new LuaBoolean { value = !Equals(obj) };
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