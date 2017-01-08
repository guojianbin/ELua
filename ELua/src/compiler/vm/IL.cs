﻿using System;

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
			Negate, Multiply, Division, Mod, Plus, Subtract,
			Property, Index, Call,
            Table, List, List0,
			Ret,

		}

		public OpCode opCode;
		public LuaObject opArg;

		public void Run(StackFrame stackFrame) {
			switch (opCode) {
				case OpCode.Undefine:
					break;
				case OpCode.Push:
					stackFrame.Push(opArg);
					break;
				case OpCode.Pop:
			        stackFrame.Pop();
					break;
				case OpCode.Save:
					stackFrame.Save((LuaVar)opArg);
					break;
				case OpCode.Jump:
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
				case OpCode.Property:
                    stackFrame.Push(stackFrame.Find(((LuaVar)stackFrame.Pop()).value).GetProperty((LuaVar)stackFrame.Pop()));
					break;
                case OpCode.List0:
                    stackFrame.Push(new LuaTable { IsArray = true, IsInit = false });
                    break;
                case OpCode.List:
                    stackFrame.Push(LuaTable.CreateList(stackFrame.TakeAll()));
			        break;
                case OpCode.Table:
                    stackFrame.Push(LuaTable.CreateTable(stackFrame.TakeAll()));
			        break;
				case OpCode.Index:
					break;
				case OpCode.Call:
			        if (stackFrame.stackLen == 0) {
			            opArg.Call(stackFrame);
			        } else {
			            opArg.Call(stackFrame, stackFrame.TakeAll());
			        }
					break;
				case OpCode.Ret:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override string ToString() {
			if (opArg == null) {
				return opCode.ToString();
			} else {
				return string.Format("{0} {1}", opCode, opArg);
			}
		}

	}

}