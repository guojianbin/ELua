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

			Undef,
			Push, Pop, Bind, BindN, Clear, Unpack, Concat, Define,
            Jump, JumpIf, JumpNot, Label, 
            Negate, Multiply, Division, Mod, Plus, Subtract,
            Not, And, Or,
            Less, Greater, LessEqual, GreaterEqual, Equal, NotEqual,
			Property, Index, Call, Function, Var,
            Table, List,
			Return,

		}

	    public int index;
		public OpCode opCode;
		public LuaObject opArg;

		public void Execute(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Push: {
					stackFrame.Push(opArg);
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
	                var item1 = stackFrame.PopVar();
	                var item2 = stackFrame.Pop();
			        item1.Bind(item2);
                    stackFrame.Push(item1);
	                break;
                }
				case OpCode.BindN: {
					var len = ((LuaInteger)opArg).value;
					var list1 = stackFrame.TakeVars(len);
					var list2 = stackFrame.Take(len);
					for (var i = 0; i < len; i++) {
					    list1[i].Bind(list2[i]);
                        stackFrame.Push(list1[i]);
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
	                stackFrame.Push(item.Not());
	                break;
                }
                case OpCode.And: {
	                var item1 = stackFrame.Pop();
	                var item2 = stackFrame.Pop();
	                stackFrame.Push(item1.And(item2));
	                break;
                }
				case OpCode.Or: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.Or(item2));
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
					stackFrame.Push(item1.Equal(item2));
					break;
				}
				case OpCode.NotEqual: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.NotEqual(item2));
					break;
				}
				case OpCode.Property: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.GetProperty(item2));
					break;
				}
				case OpCode.Index: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(item1.GetIndex(item2));
	                break;
					}
				case OpCode.Concat: {
					var item1 = stackFrame.Pop();
					var item2 = stackFrame.Pop();
					stackFrame.Push(stackFrame.vm.GetString(string.Concat(item1.ToString(), item2.ToString())));
					break;
				}
                case OpCode.List: {
	                var len = ((LuaInteger)opArg).value;
	                var list = stackFrame.Take(len);
	                stackFrame.Push(TableHelper.CreateList(stackFrame, list));
	                break;
                }
                case OpCode.Table: {
	                var len = ((LuaInteger)opArg).value;
	                var list = stackFrame.Take(len);
	                stackFrame.Push(TableHelper.CreateTable(stackFrame, list));
	                break;
                }
				case OpCode.Call: {
					var item = stackFrame.Pop();
			        var list = stackFrame.Take(((LuaInteger)opArg).value);
					item.Call(stackFrame, list);
					break;
				}
				case OpCode.Define: {
					var item = (LuaString)opArg;
					var binder = stackFrame.Define(item.value);
			        stackFrame.Push(stackFrame.vm.GetVar(item.value, binder));
					break;
				}
				case OpCode.Var: {
					var item = (LuaString)opArg;
					var binder = stackFrame.Find(item.value);
			        stackFrame.Push(stackFrame.vm.GetVar(item.value, binder));
					break;
				}
				case OpCode.Function: {
					var item = (LuaModule)opArg;
			        stackFrame.Push(stackFrame.vm.GetFunction(item.value, stackFrame));
					break;
				}
				case OpCode.Return: {
					stackFrame.Return();
					break;
				}
				case OpCode.Jump: {
					stackFrame.Jump((LuaLabel)opArg);
					break;
				}
				case OpCode.JumpIf: {
					if (stackFrame.Pop().ToBoolean()) {
						stackFrame.Jump((LuaLabel)opArg);
					}
					break;
				}
				case OpCode.JumpNot: {
					if (!stackFrame.Pop().ToBoolean()) {
						stackFrame.Jump((LuaLabel)opArg);
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
			if (opArg == null) {
				return string.Format("{0,-3} {1}", index, opCode.ToString());
			} else {
				return string.Format("{2,-3} {0,-10} {1,-10}", opCode, opArg, index);
			}
		}

	}

}