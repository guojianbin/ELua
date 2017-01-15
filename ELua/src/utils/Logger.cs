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
	    public struct Message {

	        public string content;
	        public Type type;

	        public Message(string content, Type type) {
	            this.content = content;
	            this.type = type;
	        }

	    }

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			Undefine, Console, File, All

		}

		public StreamWriter logWriter;
	    public Queue<Message> msgQueue = new Queue<Message>();

	    public Logger() {
            // ignored
	    }

	    public Logger(string file) {
			logWriter = new StreamWriter(file);
		}

		public void WriteLine(string msg, Type type = Type.All) {
            msgQueue.Enqueue(new Message(msg, type));
		}

	    public void Flush() {
	        Write();
	        logWriter.Flush();
	    }

	    public void Write() {
	        while (msgQueue.Count > 0) {
	            var msg = msgQueue.Dequeue();
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

}