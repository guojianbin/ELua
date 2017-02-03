using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public static class ExpressionCreator {

		public static Expression CreateWordEx(List<Expression> list, int position, int len) {
			return new WordExExpression(list, position, len);
		}

		public static Expression CreateWordList1(List<Expression> list, int position, int len) {
			return new WordList1Expression(list, position, len);
		}

		public static Expression CreateWordList2(List<Expression> list, int position, int len) {
			return new WordList2Expression(list, position, len);
		}

		public static Expression CreateLeftEx(List<Expression> list, int position, int len) {
			return new LeftExExpression(list, position, len);
		}

		public static Expression CreateLeftList1(List<Expression> list, int position, int len) {
			return new LeftList1Expression(list, position, len);
		}

		public static Expression CreateLeftList2(List<Expression> list, int position, int len) {
			return new LeftList2Expression(list, position, len);
		}

		public static Expression CreateRightEx(List<Expression> list, int position, int len) {
			return new RightExExpression(list, position, len);
		}

		public static Expression CreateRightList1(List<Expression> list, int position, int len) {
			return new RightList1Expression(list, position, len);
		}

		public static Expression CreateRightList2(List<Expression> list, int position, int len) {
			return new RightList2Expression(list, position, len);
		}

		public static Expression CreateKV1(List<Expression> list, int position, int len) {
			return new KV1Expression(list, position, len);
		}

		public static Expression CreateKV2(List<Expression> list, int position, int len) {
			return new KV2Expression(list, position, len);
		}

		public static Expression CreateKVList1(List<Expression> list, int position, int len) {
			return new KVList1Expression(list, position, len);
		}

		public static Expression CreateKVList2(List<Expression> list, int position, int len) {
			return new KVList2Expression(list, position, len);
		}

		public static Expression CreateModule(List<Expression> list, int position, int len) {
			return new ModuleExpression(list, position, len);
		}

		public static Expression CreateBreak(List<Expression> list, int position, int len) {
			return new BreakExpression(list, position, len);
		}

		public static Expression CreateDo(List<Expression> list, int position, int len) {
			return new DoExpression(list, position, len);
		}

		public static Expression CreateWhile(List<Expression> list, int position, int len) {
			return new WhileExpression(list, position, len);
		}

		public static Expression CreateForN(List<Expression> list, int position, int len) {
			return new ForNExpression(list, position, len);
		}

		public static Expression CreateFor(List<Expression> list, int position, int len) {
			return new ForExpression(list, position, len);
		}

		public static Expression CreateForEach(List<Expression> list, int position, int len) {
			return new ForEachExpression(list, position, len);
		}

		public static Expression CreateDefineN(List<Expression> list, int position, int len) {
			return new DefineNExpression(list, position, len);
		}

		public static Expression CreateReturn(List<Expression> list, int position, int len) {
			return new ReturnExpression(list, position, len);
		}

		public static Expression CreateFunctionA(List<Expression> list, int position, int len) {
			return new FunctionAExpression(list, position, len);
		}

		public static Expression CreateFunctionN(List<Expression> list, int position, int len) {
			return new FunctionNExpression(list, position, len);
		}

		public static Expression CreateFunctionS(List<Expression> list, int position, int len) {
			return new FunctionSExpression(list, position, len);
		}

		public static Expression CreateIf(List<Expression> list, int position, int len) {
			return new IfExpression(list, position, len);
		}

		public static Expression CreateIfElse(List<Expression> list, int position, int len) {
			return new IfElseExpression(list, position, len);
		}

		public static Expression CreateParen(List<Expression> list, int position, int len) {
			return new ParenExpression(list, position, len);
		}

		public static Expression CreateTableI(List<Expression> list, int position, int len) {
			return new TableIExpression(list, position, len);
		}

		public static Expression CreateTableS(List<Expression> list, int position, int len) {
			return new TableSExpression(list, position, len);
		}

		public static Expression CreateList(List<Expression> list, int position, int len) {
			return new ListExpression(list, position, len);
		}

		public static Expression CreateProperty(List<Expression> list, int position, int len) {
			return new PropertyExpression(list, position, len);
		}

		public static Expression CreateIndex(List<Expression> list, int position, int len) {
			return new IndexExpression(list, position, len);
		}

		public static Expression CreateCall(List<Expression> list, int position, int len) {
			return new CallExpression(list, position, len);
		}

		public static Expression CreateNot(List<Expression> list, int position, int len) {
			return new NotExpression(list, position, len);
		}

		public static Expression CreateLength(List<Expression> list, int position, int len) {
			return new LengthExpression(list, position, len);
		}

		public static Expression CreateNegate(List<Expression> list, int position, int len) {
			return new NegateExpression(list, position, len);
		}

		public static Expression CreatePower(List<Expression> list, int position, int len) {
			return new PowerExpression(list, position, len);
		}

		public static Expression CreateMultiply(List<Expression> list, int position, int len) {
			return new MultiplyExpression(list, position, len);
		}

		public static Expression CreateDivision(List<Expression> list, int position, int len) {
			return new DivisionExpression(list, position, len);
		}

		public static Expression CreateMod(List<Expression> list, int position, int len) {
			return new ModExpression(list, position, len);
		}

		public static Expression CreateAdd(List<Expression> list, int position, int len) {
			return new AddExpression(list, position, len);
		}

		public static Expression CreateSubtract(List<Expression> list, int position, int len) {
			return new SubtractExpression(list, position, len);
		}

		public static Expression CreateConcat(List<Expression> list, int position, int len) {
			return new ConcatExpression(list, position, len);
		}

		public static Expression CreateLess(List<Expression> list, int position, int len) {
			return new LessExpression(list, position, len);
		}

		public static Expression CreateGreater(List<Expression> list, int position, int len) {
			return new GreaterExpression(list, position, len);
		}

		public static Expression CreateLessEqual(List<Expression> list, int position, int len) {
			return new LessEqualExpression(list, position, len);
		}

		public static Expression CreateGreaterEqual(List<Expression> list, int position, int len) {
			return new GreaterEqualExpression(list, position, len);
		}

		public static Expression CreateEqual(List<Expression> list, int position, int len) {
			return new EqualExpression(list, position, len);
		}

		public static Expression CreateNotEqual(List<Expression> list, int position, int len) {
			return new NotEqualExpression(list, position, len);
		}

		public static Expression CreateAnd(List<Expression> list, int position, int len) {
			return new AndExpression(list, position, len);
		}

		public static Expression CreateOr(List<Expression> list, int position, int len) {
			return new OrExpression(list, position, len);
		}

		public static Expression CreateBind(List<Expression> list, int position, int len) {
			return new BindExpression(list, position, len);
		}

		public static Expression CreateBindN(List<Expression> list, int position, int len) {
			return new BindNExpression(list, position, len);
		}

    }

}