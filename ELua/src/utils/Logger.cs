using System;
using System.Collections.Generic;
using System.IO;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Logger {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type : byte {

			Undef, Console, File, All

		}

	    /// <summary>
	    /// @author Easily
	    /// </summary>
	    public struct Message {

	        public string content;
	        public Type type;

	        public Message(string content, Type type) {
	            this.content = content;
	            this.type = type;
	        }

	    }

		public bool IsTest;
		public StreamWriter logWriter;
	    public Queue<Message> msgQueue = new Queue<Message>();

	    public Logger() {
		    IsTest = true;
	    }

	    public Logger(string file) {
			logWriter = new StreamWriter(file);
		}

		public void WriteLine(string msg, Type type = Type.All) {
            msgQueue.Enqueue(new Message(msg, type));
			if (!IsTest) {
				Flush();
			}
		}

		public void Flush() {
			while (msgQueue.Count > 0) {
				Write(msgQueue.Dequeue());
			}
	        logWriter.Flush();
	    }

		public void Write(Message msg) {
			switch (msg.type) {
				case Type.Console:
					Console.WriteLine(msg.content);
					break;
				case Type.File:
					logWriter.WriteLine(msg.content);
					break;
				case Type.All:
					Console.WriteLine(msg.content);
					logWriter.WriteLine(msg.content);
					break;
			}
		}

	}

}