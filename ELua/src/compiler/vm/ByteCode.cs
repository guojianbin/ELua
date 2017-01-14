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
			Push, Pop, Bind, BindN, Clear, Unpack,
            Jump, JumpIf, JumpNot, Label, 
            Negate, Multiply, Division, Mod, Plus, Subtract,
            Not, And, Or,
            Less, Greater, LessEqual, GreaterEqual, Equal, NotEqual,
			Property, Index, Call, Function,
            Table, List,
			Return,

		}

	    public int index;
		public OpCode opCode;
		public LuaObject opArg1;
		public LuaObject opArg2;

		public void Execute(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Push: {
					stackFrame.Push(opArg1);
					break;
				}
				case OpCode.Pop: {
					stackFrame.Pop();
					break;
				}
                case OpCode.Clear: {
	                stackFrame.Clear();
	                break;
                }
                case OpCode.Bind: {
	                var item1 = stackFrame.PopRaw();
	                var item2 = stackFrame.Pop();
	                stackFrame.Push(item1.Bind(stackFrame, item2));
	                break;
                }
				case OpCode.BindN: {
					var len = ((LuaInteger)opArg1).value;
					var list1 = stackFrame.TakeRaw(len);
					var list2 = stackFrame.Take(len);
					for (var i = 0; i < len; i++) {
						stackFrame.Push(list1[i].Bind(stackFrame, list2[i]));
					}
					break;
				}
				case OpCode.Unpack: {
					var item = stackFrame.Pop();
					item.Unpack(stackFrame);
					break;
				}
                case OpCode.Not: {
	                var item = stackFrame.Pop();
	                stackFrame.Push(item.Not(stackFrame));
	                break;
                }
                case OpCode.And: {
	                var item1 = stackFrame.Pop();
	                var item2 = stackFrame.Pop();
	                stackFrame.Push(item1.And(stackFrame, item2));
	                break;
                }
				case OpCode.Or: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Or(stackFrame, item2));
	                break;
                }
				case OpCode.Negate: {
					var item = stackFrame.Pop();
					stackFrame.Push(item.Negate(stackFrame));
					break;
				}
				case OpCode.Multiply: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Multiply(stackFrame, item2));
					break;
				}
				case OpCode.Division: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Division(stackFrame, item2));
					break;
				}
				case OpCode.Mod: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Mod(stackFrame, item2));
					break;
				}
				case OpCode.Plus: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Plus(stackFrame, item2));
					break;
				}
				case OpCode.Subtract: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Subtract(stackFrame, item2));
					break;
				}
				case OpCode.Less: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Less(stackFrame, item2));
					break;
				}
				case OpCode.Greater: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Greater(stackFrame, item2));
					break;
				}
				case OpCode.LessEqual: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.LessEqual(stackFrame, item2));
					break;
				}
				case OpCode.GreaterEqual: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.GreaterEqual(stackFrame, item2));
					break;
				}
				case OpCode.Equal: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Equal(stackFrame, item2));
					break;
				}
				case OpCode.NotEqual: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.NotEqual(stackFrame, item2));
					break;
				}
				case OpCode.Property: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.GetProperty(stackFrame, item2));
					break;
				}
				case OpCode.Index: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.GetIndex(stackFrame, item2));
	                break;
                }
                case OpCode.List: {
	                var len = ((LuaInteger)opArg1).value;
	                var list = stackFrame.Take(len);
	                stackFrame.Push(TypeHelper.CreateList(stackFrame, list));
	                break;
                }
                case OpCode.Table: {
	                var len = ((LuaInteger)opArg1).value;
	                var list = stackFrame.Take(len);
	                stackFrame.Push(TypeHelper.CreateTable(stackFrame, list));
	                break;
                }
				case OpCode.Call: {
					var item = stackFrame.Pop();
					var list = stackFrame.TakeAll();
					item.Call(stackFrame, list);
					break;
				}
				case OpCode.Function: {
					var item = (LuaModule)opArg1;
			        stackFrame.Push(stackFrame.vm.GetFunction(item.value));
					break;
				}
				case OpCode.Return: {
					stackFrame.Return();
					break;
				}
				case OpCode.Jump: {
					stackFrame.Jump((LuaLabel)opArg1);
					break;
				}
				case OpCode.JumpIf: {
					if (stackFrame.Pop().ToBoolean(stackFrame)) {
						stackFrame.Jump((LuaLabel)opArg1);
					}
					break;
				}
				case OpCode.JumpNot: {
					if (!stackFrame.Pop().ToBoolean(stackFrame)) {
						stackFrame.Jump((LuaLabel)opArg1);
					}
					break;
				}
				case OpCode.Label: {
					// ignored
					break;
				}
				default:
					throw new ArgumentOutOfRangeException(opCode.ToString());
			}
		}

		public override string ToString() {
			if (opArg1 == null) {
				return string.Format("{0,-3} {1}", index, opCode.ToString());
			} else if (opArg2 == null) {
				return string.Format("{2,-3} {0,-10} {1,-10}", opCode, opArg1, index);
			} else {
				return string.Format("{3,-3} {0,-10} {1,-10} {2,-10}", opCode, opArg1, opArg2, index);
			}
		}

	}

}