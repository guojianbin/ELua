using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WordExExpression : Expression {

		public Expression targetExp;

		public WordExExpression(List<Expression> list, int position, int len) {
			type = Type.WordEx;
			targetExp = list[position];
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return targetExp.ToString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WordList1Expression : Expression {

		public List<Expression> itemsList;

		public WordList1Expression(List<Expression> list, int position, int len) {
			type = Type.WordList1;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(((WordExExpression)list[position + i]).targetExp);
			}
		}

		public string[] ToParams() {
			return itemsList.Cast<WordExpression>().Select(t => t.value).ToArray();
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WordList2Expression : Expression {

		public List<Expression> itemsList;

		public WordList2Expression(List<Expression> list, int position, int len) {
			type = Type.WordList2;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(((WordExExpression)list[position + i]).targetExp);
			}
		}

		public WordList2Expression(List<Expression> itemsList) {
			this.itemsList = itemsList;
		}

		public void ToVars() {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = new LocalExpression(itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LeftExExpression : Expression {

		public Expression targetExp;

		public LeftExExpression(List<Expression> list, int position, int len) {
			type = Type.LeftEx;
			targetExp = list[position];
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return targetExp.ToString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LeftList1Expression : Expression {

		public List<Expression> itemsList;

		public LeftList1Expression(List<Expression> list, int position, int len) {
			type = Type.LeftList1;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				var targetExp = ((LeftExExpression)list[position + i]).targetExp;
				targetExp.isBinder = true;
				itemsList.Add(targetExp);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class LeftList2Expression : Expression {

		public List<Expression> itemsList;

		public LeftList2Expression(List<Expression> list, int position, int len) {
			type = Type.LeftList2;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				var targetExp = ((LeftExExpression)list[position + i]).targetExp;
				targetExp.isBinder = true;
				itemsList.Add(targetExp);
			}
		}

	    public LeftList2Expression(List<Expression> itemsList) {
	        this.itemsList = itemsList;
	    }

	    public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

    /// <summary>
    /// @author Easily
    /// </summary>
    public class RightExExpression : Expression {

        public Expression targetExp;

		public RightExExpression(List<Expression> list, int position, int len) {
            type = Type.RightEx;
            targetExp = list[position];
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

        public override string ToString() {
			return targetExp.ToString();
        }

    }

    /// <summary>
    /// @author Easily
    /// </summary>
    public class RightList1Expression : Expression {

        public List<Expression> itemsList;

		public RightList1Expression(List<Expression> list, int position, int len) {
            type = Type.RightList1;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(((RightExExpression)list[position + i]).targetExp);
			}
		}

	    public RightList1Expression(List<Expression> itemsList) {
		    this.itemsList = itemsList;
	    }

	    public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

        public override string ToString() {
            return itemsList.FormatListString();
        }

    }

    /// <summary>
    /// @author Easily
    /// </summary>
    public class RightList2Expression : Expression {

        public List<Expression> itemsList;

		public RightList2Expression(List<Expression> list, int position, int len) {
            type = Type.RightList2;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(((RightExExpression)list[position + i]).targetExp);
			}
		}

	    public RightList2Expression(List<Expression> itemsList) {
		    this.itemsList = itemsList;
	    }

	    public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i] = ParserHelper.Extract(context, itemsList[i]);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

        public override string ToString() {
            return itemsList.FormatListString();
        }

    }

	/// <summary>
	/// @author Easily
	/// </summary>
	public class KV1Expression : Expression {

		public Expression keyExp;
		public Expression valueExp;

		public KV1Expression(List<Expression> list, int position, int len) {
			type = Type.KV1;
			keyExp = ParserHelper.Word2String(list[position]);
			valueExp = list[position + 2];
		}

		public override void Extract(SyntaxContext context) {
			keyExp = ParserHelper.Extract(context, keyExp);
			valueExp = ParserHelper.Extract(context, valueExp);
		}

		public override void Generate(ModuleContext context) {
			valueExp.Generate(context);
			keyExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(keyExp, valueExp);
		}

		public override string ToString() {
			return string.Format("[{0}]={1}", keyExp, valueExp);
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class KVList1Expression : Expression {

		public List<Expression> itemsList;

		public KVList1Expression(List<Expression> list, int position, int len) {
			type = Type.KVList1;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(list[position + i]);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i].Extract(context);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class KV2Expression : Expression {

		public Expression keyExp;
		public Expression valueExp;

		public KV2Expression(List<Expression> list, int position, int len) {
			type = Type.KV2;
			keyExp = list[position + 1];
			valueExp = list[position + 4];
		}

		public override void Extract(SyntaxContext context) {
			keyExp = ParserHelper.Extract(context, keyExp);
			valueExp = ParserHelper.Extract(context, valueExp);
		}

		public override void Generate(ModuleContext context) {
			valueExp.Generate(context);
			keyExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(keyExp, valueExp);
		}

		public override string ToString() {
			return string.Format("[{0}]={1}", keyExp, valueExp);
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class KVList2Expression : Expression {

		public List<Expression> itemsList;

		public KVList2Expression(List<Expression> list, int position, int len) {
			type = Type.KVList2;
			itemsList = new List<Expression>();
			for (var i = 0; i < len; i++) {
				itemsList.Add(list[position + i]);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < itemsList.Count; i++) {
				itemsList[i].Extract(context);
			}
		}

		public override void Generate(ModuleContext context) {
			for (var i = itemsList.Count - 1; i >= 0; i--) {
				itemsList[i].Generate(context);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList.ToArray());
		}

		public override string ToString() {
			return itemsList.FormatListString();
		}

	}

}