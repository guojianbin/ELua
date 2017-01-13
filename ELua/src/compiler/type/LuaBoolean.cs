﻿namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LuaBoolean : LuaObject {

		public bool value;

		public LuaBoolean(LVM vm, bool value) : base(vm) {
			this.value = value;
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
			return value;
		}

		public override string ToString() {
			return value.ToString();
		}

		public override int GetHashCode() {
			return value.GetHashCode();
		}

		protected bool Equals(LuaBoolean other) {
			return value.Equals(other.value);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			} else if (ReferenceEquals(this, obj)) {
				return true;
			} else if (obj.GetType() != GetType()) {
				return false;
			} else {
				return Equals((LuaBoolean)obj);
			}
		}

	}

}