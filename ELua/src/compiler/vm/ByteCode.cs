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
            Table, List, Function,
			Return,

		}

	    public int index;
		public OpCode opCode;
		public LuaObject opArg1;
		public LuaObject opArg2;

		public void Execute(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Push:
					stackFrame.Push(opArg1);
					break;
				case OpCode.Pop:
			        stackFrame.Pop();
					break;
                case OpCode.Clear:
                    stackFrame.Clear();
                    break;
                case OpCode.Bind:
					stackFrame.Push(stackFrame.PopRaw().Bind(stackFrame, stackFrame.Pop()));
                    break;
                case OpCode.Label:
                    // ignored
                    break;
                case OpCode.Jump:
                    stackFrame.Jump((LuaLabel)opArg1);
                    break;
                case OpCode.JumpIf:
			        if (stackFrame.Pop().ToBoolean(stackFrame)) {
						stackFrame.Jump((LuaLabel)opArg1);
                    }
					break;
                case OpCode.JumpNot:
			        if (!stackFrame.Pop().ToBoolean(stackFrame)) {
						stackFrame.Jump((LuaLabel)opArg1);
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
                case OpCode.List:
					stackFrame.Push(TypeHelper.CreateList(stackFrame, stackFrame.Take(((LuaInteger)opArg1).value)));
			        break;
                case OpCode.Table:
					stackFrame.Push(TypeHelper.CreateTable(stackFrame, stackFrame.Take(((LuaInteger)opArg1).value)));
					break;
				case OpCode.Function:
					stackFrame.Push(TypeHelper.CreateFunction(stackFrame, ((LuaString)opArg1).value, ((LuaArgs)opArg2).argsList));
					break;
				case OpCode.Call:
					stackFrame.Push(stackFrame.Pop().Call(stackFrame, stackFrame.Take(((LuaInteger)opArg1).value)));
					break;
				case OpCode.Return:
					stackFrame.Return();
					break;
				default:
					throw new ArgumentOutOfRangeException(opCode.ToString());
			}
		}

		public override string ToString() {
			if (opArg1 == null) {
				return opCode.ToString();
			} else if (opArg2 == null) {
				return string.Format("{0,-10} {1,-10}", opCode, opArg1);
			} else {
				return string.Format("{0,-10} {1,-10} {2,-10}", opCode, opArg1, opArg2);
			}
		}

	}

}