using System;
using System.IO;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Logger {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			Undefine, Console, File, All

		}

		public StreamWriter logWriter;

		public Logger(string file) {
			logWriter = new StreamWriter(file);
		}

		public void WriteLine(string msg, Type type = Type.All) {
			switch (type) {
				case Type.Console:
					Console.WriteLine(msg);
					break;
				case Type.File:
					logWriter.WriteLine(msg);
					logWriter.Flush();
					break;
				case Type.All:
					Console.WriteLine(msg);
					logWriter.WriteLine(msg);
					logWriter.Flush();
					break;
				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

	}

}