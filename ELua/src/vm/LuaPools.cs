using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaPools {

        public LVM vm;

        public LuaPools(LVM vm) {
            this.vm = vm;
        }

        public LuaNumber GetNumber(float value) {
            return new LuaNumber(vm, value);
        }

        public LuaBoolean GetBoolean(bool value) {
            return new LuaBoolean(vm, value);
        }

        public LuaString GetString(string value) {
            return new LuaString(vm, value);
        }

        public LuaFunction GetFunction(Module module, StackFrame stackFrame) {
            return new LuaFunction(vm, module, stackFrame);
        }

        public LuaVar GetVar(string name, LuaBinder binder) {
            return new LuaVar(vm, name, binder);
        }

        public LuaTable GetTable() {
            return new LuaTable(vm);
        }

        public LuaUserdata GetUserdata(object value) {
            return new LuaUserdata(vm, value);
        }

        public LuaTuple GetTuple(IEnumerable<LuaObject> list) {
            return new LuaTuple(vm, list);
        }

        public LuaListItem GetListItem(LuaTable table, LuaList list, int index, LuaObject value) {
            return new LuaListItem(vm, table, list, index, value);
        }

        public LuaDictItem GetDictItem(LuaTable table, LuaDict dict, LuaObject key, LuaObject value) {
            return new LuaDictItem(vm, table, dict, key, value);
        }

        public StackFrame GetStackFrame(Module module, StackFrame parent) {
            return new StackFrame(module, parent);
        }

        public StackFrame GetStackFrame(Module module, StackFrame parent, StackFrame upvalue) {
            return new StackFrame(module, parent, upvalue);
        }

    }

}