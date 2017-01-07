using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static Expression ToExpression(this Token token) {
			switch (token.type) {
				case Token.Type.Word:
					return new WordExpression(token.value, token.debugInfo);
				case Token.Type.String:
					return new StringExpression(token.value, token.debugInfo);
				case Token.Type.Number:
					return new NumberExpression(token.value, token.debugInfo);
				case Token.Type.Keyword:
					return new Expression { IsFinally = true, type = Expression.Type.Keyword, value = token.value, debugInfo = token.debugInfo };
				case Token.Type.Operator:
					return new Expression { IsFinally = true, type = Expression.Type.Operator, value = token.value, debugInfo = token.debugInfo };
				default:
					throw new ArgumentOutOfRangeException(token.ToString());
			}
		}

	}

}