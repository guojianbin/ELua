using System;
namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class IL {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum OpCode {

			Undefine,
			Push, Pop, Save, Jump,
			Multiply, Division, Mod, Plus, Subtract,
			Property, Index, Call,
			Bind,
			Ret,

		}

		public OpCode opCode;
		public LuaObject arg1;
		public LuaObject arg2;

		public void Run(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Undefine:
					break;
				case OpCode.Push:
					stackFrame.Push(arg1);
					break;
				case OpCode.Pop:
					break;
				case OpCode.Save:
					stackFrame.Save((LuaVar)arg1);
					break;
				case OpCode.Jump:
					break;
				case OpCode.Multiply:
					break;
				case OpCode.Division:
					break;
				case OpCode.Mod:
					break;
				case OpCode.Plus:
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Plus(item2));
					break;
				case OpCode.Subtract:
					break;
				case OpCode.Property:
					break;
				case OpCode.Index:
					break;
				case OpCode.Call:
					((LuaVar)arg1).Call(stackFrame, stackFrame.TakeAll());
					break;
				case OpCode.Bind:
					break;
				case OpCode.Ret:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override string ToString() {
			if (arg1 == null) {
				return opCode.ToString();
			} else if (arg2 == null) {
				return string.Format("{0} {1}", opCode, arg1);
			} else {
				return string.Format("{0} {1}, {2}", opCode, arg1, arg2);
			}
		}

	}

}