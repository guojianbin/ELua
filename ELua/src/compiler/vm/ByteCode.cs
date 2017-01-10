using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public struct ByteCode {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum OpCode {

			Undefine,
			Push, Pop, Bind, Clear,
            Jump, JumpIf, JumpNot, Label, 
            Negate, Multiply, Division, Mod, Plus, Subtract,
            Not, And, Or,
            Less, Greater, LessEqual, GreaterEqual, Equal, NotEqual,
			Property, Index, Call, 
            Table, List, List0,
			Return,

		}

	    public int index;
		public OpCode opCode;
		public LuaObject opArg;

		public void Execute(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Push:
					stackFrame.Push(opArg);
					break;
				case OpCode.Pop:
			        stackFrame.Pop();
					break;
                case OpCode.Clear:
                    stackFrame.Clear();
                    break;
                case OpCode.Bind:
                    stackFrame.Push(stackFrame.Pop().Bind(stackFrame, stackFrame.Pop()));
                    break;
                case OpCode.Label:
                    // ignored
                    break;
                case OpCode.Jump:
                    stackFrame.module.Jump((LuaLabel)opArg);
                    break;
                case OpCode.JumpIf:
			        if (stackFrame.Pop().ToBoolean(stackFrame)) {
                        stackFrame.module.Jump((LuaLabel)opArg);
                    }
					break;
                case OpCode.JumpNot:
			        if (!stackFrame.Pop().ToBoolean(stackFrame)) {
                        stackFrame.module.Jump((LuaLabel)opArg);
                    }
					break;
                case OpCode.Not:
                    stackFrame.Push(stackFrame.Pop().Not(stackFrame));
                    break;
                case OpCode.And:
                    stackFrame.Push(stackFrame.Pop().And(stackFrame, stackFrame.Pop()));
                    break;
                case OpCode.Or:
                    stackFrame.Push(stackFrame.Pop().Or(stackFrame, stackFrame.Pop()));
                    break;
                case OpCode.Negate:
                    stackFrame.Push(stackFrame.Pop().Negate(stackFrame));
                    break;
                case OpCode.Multiply:
                    stackFrame.Push(stackFrame.Pop().Multiply(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Division:
                    stackFrame.Push(stackFrame.Pop().Division(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Mod:
                    stackFrame.Push(stackFrame.Pop().Mod(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Plus:
			        stackFrame.Push(stackFrame.Pop().Plus(stackFrame, stackFrame.Pop()));
					break;
				case OpCode.Subtract:
                    stackFrame.Push(stackFrame.Pop().Subtract(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Less:
                    stackFrame.Push(stackFrame.Pop().Less(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Greater:
                    stackFrame.Push(stackFrame.Pop().Greater(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.LessEqual:
                    stackFrame.Push(stackFrame.Pop().LessEqual(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.GreaterEqual:
                    stackFrame.Push(stackFrame.Pop().GreaterEqual(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Equal:
                    stackFrame.Push(stackFrame.Pop().Equal(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.NotEqual:
                    stackFrame.Push(stackFrame.Pop().NotEqual(stackFrame, stackFrame.Pop()));
                    break;
				case OpCode.Property:
                    stackFrame.Push(stackFrame.Pop().GetProperty(stackFrame, stackFrame.Pop()));
					break;
                case OpCode.Index:
                    stackFrame.Push(stackFrame.Pop().GetIndex(stackFrame, stackFrame.Pop()));
                    break;
                case OpCode.List0:
                    stackFrame.Push(TableHelper.CreateEmpty());
                    break;
                case OpCode.List:
                    stackFrame.Push(TableHelper.CreateList(stackFrame.TakeAll()));
			        break;
                case OpCode.Table:
                    stackFrame.Push(TableHelper.CreateTable(stackFrame.TakeAll()));
			        break;
				case OpCode.Call:
					stackFrame.Push(opArg.Call(stackFrame, stackFrame.TakeAll()));
					break;
				case OpCode.Return:
					stackFrame.module.Return();
					break;
				default:
					throw new ArgumentOutOfRangeException(opCode.ToString());
			}
		}

		public override string ToString() {
			if (opArg == null) {
				return opCode.ToString();
			} else {
				return string.Format("{0,-10} {1}", opCode, opArg);
			}
		}

	}

}