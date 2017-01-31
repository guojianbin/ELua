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
		public bool isExecuting;

		public Executor(StackFrame stackFrame) {
			this.stackFrame = stackFrame;
			uid = stackFrame.uid;
			vm = stackFrame.vm;
		}

		public void Start() {
			isExecuting = true;
			currentFrame = stackFrame;
			Execute();
		}

		public void Execute(StackFrame stackFrame) {
			callStack.Push(currentFrame);
			currentFrame = stackFrame;
		}

		public void Execute() {
			while (true) {
				if (isExecuting) {
					if (!currentFrame.MoveNext()) {
						isExecuting = false;
					}
				} else {
					if (callStack.Count == 0) {
						isExecuting = false;
						vm.Remove(this);
						break;
					} else {
						currentFrame = callStack.Pop();
						isExecuting = true;
					}
				}
			}
		}

	}

}