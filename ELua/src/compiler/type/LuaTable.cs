using System;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public LuaDict dict;
        public LuaList list;
        public bool IsList;
        public bool IsInit;

        public override LuaObject GetProperty(StackFrame stackFrame, LuaObject obj) {
            if (IsNil) {
                throw new NullReferenceException();
            } else if (!IsInit) {
                return stackFrame.vm.nil;
            } else if (IsList) {
                return list.GetProperty(stackFrame, obj);
            } else {
                return dict.GetProperty(stackFrame, obj);
            }
        }

        public override LuaObject GetIndex(StackFrame stackFrame, LuaObject obj) {
            if (IsNil) {
                throw new NullReferenceException();
            } else if (!IsInit) {
                return stackFrame.vm.nil;
            } else if (IsList) {
                return list.GetIndex(stackFrame, obj);
            } else {
                return dict.GetIndex(stackFrame, obj);
            }
        }

        public override string ToString(StackFrame stackFrame) {
            if (!IsInit) {
                return "{ }";
            } else if (IsList) {
                return list.ToString(stackFrame);
            } else {
                return dict.ToString(stackFrame);
            }
        }

        public override string ToString() {
            return "table";
        }

    }

}