using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaPools {

        public LVM vm;
        public Queue<LuaNumber> numberPool = new Queue<LuaNumber>();

        public LuaPools(LVM vm) {
            this.vm = vm;
        }

        public LuaNumber GetNumber(float value) {
            if (numberPool.Count == 0) {
                return new LuaNumber {vm = vm, value = value};
            } else {
                var item = numberPool.Dequeue();
                item.value = value;
                return item;
            }
        }

        public void PutNumber(LuaNumber item) {
            numberPool.Enqueue(item);
        }

    }

}