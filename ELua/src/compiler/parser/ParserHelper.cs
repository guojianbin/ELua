using System;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class ParserHelper {

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

        public static Expression Extract(SyntaxContext context, Expression expression) {
            if (expression.IsFinally) {
                return expression;
            }
            expression.Extract(context);
            if (expression.IsLeftValue) {
                return expression;
            }
            var temp = new TempExpression(context.NewUID(), expression);
            context.Add(new DefineExpression(temp, expression));
            return temp;
        }

    }

}