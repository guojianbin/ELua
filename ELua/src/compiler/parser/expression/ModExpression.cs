﻿using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ModExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public ModExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			type = Type.Mod;
			debugInfo = list[position].debugInfo;
			item1Exp = list[position];
			item2Exp = list[position + 2];
		}

		public override void Extract(SyntaxContext context) {
			item1Exp = ParserHelper.Extract(context, item1Exp);
			item2Exp = ParserHelper.Extract(context, item2Exp);
        }

        public override void Generate(ModuleContext context) {
            item2Exp.Generate(context);
            item1Exp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Mod });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(item1Exp, item2Exp);
		}

		public override string ToString() {
			return string.Format("{0} % {1}", item1Exp, item2Exp);
		}

	}

}