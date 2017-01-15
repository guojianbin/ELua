using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Executor {

		public string uid;
		public LVM vm;
		public StackFrame stackFrame;
		public Stack<StackFrame> callStack = new Stack<StackFrame>();
		public StackFrame currentFrame;
		public bool IsExecuting;

		public Executor(StackFrame stackFrame) {
			this.stackFrame = stackFrame;
			uid = stackFrame.uid;
			vm = stackFrame.vm;
		}

		public void Start() {
			IsExecuting = true;
			currentFrame = stackFrame;
			Execute();
		}

		public void Execute(StackFrame stackFrame) {
			callStack.Push(currentFrame);
			currentFrame = stackFrame;
		}

		public void Execute() {
			while (true) {
				if (IsExecuting) {
					if (!currentFrame.MoveNext()) {
						IsExecuting = false;
					}
				} else {
					if (callStack.Count == 0) {
						IsExecuting = false;
						vm.Remove(this);
						break;
					} else {
						currentFrame = callStack.Pop();
						IsExecuting = true;
					}
				}
			}
		}

	}

}