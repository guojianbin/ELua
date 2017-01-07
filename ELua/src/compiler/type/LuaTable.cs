using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class LuaTable : LuaObject {

        public Dictionary<LuaObject, LuaObject> items = new Dictionary<LuaObject, LuaObject>();

        public override string ToString() {
            return items.ToString();
        }

    }

}