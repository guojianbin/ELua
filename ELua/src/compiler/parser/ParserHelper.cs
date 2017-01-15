using System;
using System.Collections.Generic;
using System.Linq;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class ParserHelper {

	    public static List<Expression> ToExpressionList(IList<Token> tokens) {
		    var list = new List<Expression>(tokens.Count);
			list.AddRange(tokens.Select(t => ToExpression(t)));
		    return list;
	    }

        public static Expression ToExpression(Token token) {
            if (token.type == Token.Type.Word) {
                return new WordExpression(token.value, token.debugInfo);
            } else if (token.type == Token.Type.String) {
                return new StringExpression(token.value, token.debugInfo);
            } else if (token.type == Token.Type.Number) {
                return new NumberExpression(token.value, token.debugInfo);
            } else if (token.type == Token.Type.Operator) {
                return new OperatorExpression(token.value, token.debugInfo);
            } else if (token.type == Token.Type.Keyword) {
                if (token.value == "nil") {
                    return new NilExpression(token.debugInfo);
                } else if (token.value == "true" || token.value == "false") {
                    return new BooleanExpression(token.value, token.debugInfo);
                } else {
                    return new KeywordExpression(token.value, token.debugInfo);
                }
            } else {
                throw new ArgumentOutOfRangeException(token.ToString());
            }
        }

        public static bool IsOperator(Expression expression, string value) {
            return expression.type == Expression.Type.Operator && ((OperatorExpression)expression).value == value;
        }

        public static bool IsKeyword(Expression expression, string value) {
            return expression.type == Expression.Type.Keyword && ((KeywordExpression)expression).value == value;
        }

        public static StringExpression Word2String(Expression expression) {
            var temp = (WordExpression)expression;
            return new StringExpression(temp.value, temp.debugInfo);
        }

		public static Expression Extract(SyntaxContext context, Expression expression) {
			if (expression.IsFinally) {
				return expression;
			}
			expression.Extract(context);
			if (context.IsCutting) {
				context.IsCutting = false;
				return context.cuttingExp;
			} else if (expression.IsRightValue && !expression.IsLeftValue) {
				var ret = Wrapper(expression, context.NewUID());
				context.Add(ret.Value);
				if (expression.type == Expression.Type.Call) {
					return new UnpackExpression(ret.Key);
				} else {
					return ret.Key;
				}
			} else {
				return expression;
			}
		}

		public static KeyValuePair<WordExpression, DefineExpression> Wrapper(Expression expression, string name) {
			var itemKey = new WordExpression(name, expression.debugInfo);
			var itemValue = new DefineExpression(itemKey, expression);
			return new KeyValuePair<WordExpression, DefineExpression>(itemKey, itemValue);
		}

    }

}