namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public interface IParser {

		bool Parse(SyntaxContext context, int position);

	}

}