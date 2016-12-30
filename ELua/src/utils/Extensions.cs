using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class Extensions {

		public static Expression ToExpression(this Token token) {
			switch (token.type) {
				case Token.Type.Word:
					return new Expression { IsToken = true, type = Expression.Type.Word, token = token, IsLeftValue = true, IsRightValue = true };
				case Token.Type.Keyword:
					return new Expression { IsToken = true, type = Expression.Type.Keyword, token = token };
				case Token.Type.Number:
					return new Expression { IsToken = true, type = Expression.Type.Number, token = token, IsRightValue = true };
				case Token.Type.Operator:
					return new Expression { IsToken = true, type = Expression.Type.Operator, token = token };
				case Token.Type.String:
					return new Expression { IsToken = true, type = Expression.Type.String, token = token, IsRightValue = true };
				default:
					throw new ArgumentOutOfRangeException(token.ToString());
			}
		}

	}

}