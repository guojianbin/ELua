using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class Extensions {

        public static LuaUserdata GetUserdata(this LVM vm, object value) {
            return vm.luaPools.GetUserdata(value);
        }

        public static LuaNumber GetNumber(this LVM vm, float value) {
            return vm.luaPools.GetNumber(value);
        }

        public static LuaBoolean GetBoolean(this LVM vm, bool value) {
            return vm.luaPools.GetBoolean(value);
        }

        public static LuaString GetString(this LVM vm, string value) {
            return vm.luaPools.GetString(value);
        }

        public static LuaVar GetVar(this LVM vm, string value, LuaBinder binder) {
            return vm.luaPools.GetVar(value, binder);
        }

        public static LuaFunction GetFunction(this LVM vm, Module value, StackFrame stackFrame) {
            return vm.luaPools.GetFunction(value, stackFrame);
        }

        public static LuaTable GetTable(this LVM vm) {
            return vm.luaPools.GetTable();
        }

        public static LuaTuple GetTuple(this LVM vm, IEnumerable<LuaObject> list) {
            return vm.luaPools.GetTuple(list);
        }

        public static LuaListItem GetListItem(this LVM vm, LuaTable table, LuaList list, int index, LuaObject value) {
            return vm.luaPools.GetListItem(table, list, index, value);
        }

        public static LuaDictItem GetDictItem(this LVM vm, LuaTable table, LuaDict dict, LuaObject key, LuaObject value) {
            return vm.luaPools.GetDictItem(table, dict, key, value);
        }

        public static StackFrame GetStackFrame(this LVM vm, Module module, StackFrame parent) {
            return vm.luaPools.GetStackFrame(module, parent);
        }

        public static StackFrame GetStackFrame(this LVM vm, Module module, StackFrame parent, StackFrame upvalue) {
            return vm.luaPools.GetStackFrame(module, parent, upvalue);
        }

        public static string FormatListString<T>(this IEnumerable<T> list) {
            return string.Join(", ", list.Select(t => t.ToString()));
        }

        public static string FormatListString<T>(this IEnumerable<T> list, string sep) {
            return string.Join(sep, list.Select(t => t.ToString()));
        }

    }

}