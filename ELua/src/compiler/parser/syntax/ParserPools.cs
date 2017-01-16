using System.Collections.Generic;

namespace ELua {

    /// <summary>
    /// @author Easily
    /// auto generated! don't modify !
    /// </summary>
    public static class ParserPools {

        public static Queue<ModuleParser> ModuleParserPool = new Queue<ModuleParser>();
        public static Queue<BreakParser> BreakParserPool = new Queue<BreakParser>();
        public static Queue<WhileParser> WhileParserPool = new Queue<WhileParser>();
        public static Queue<ForNParser> ForNParserPool = new Queue<ForNParser>();
        public static Queue<ForParser> ForParserPool = new Queue<ForParser>();
        public static Queue<ForEachParser> ForEachParserPool = new Queue<ForEachParser>();
        public static Queue<DefineParser> DefineParserPool = new Queue<DefineParser>();
        public static Queue<DefineNParser> DefineNParserPool = new Queue<DefineNParser>();
        public static Queue<BindParser> BindParserPool = new Queue<BindParser>();
        public static Queue<BindNParser> BindNParserPool = new Queue<BindNParser>();
        public static Queue<ReturnNParser> ReturnNParserPool = new Queue<ReturnNParser>();
        public static Queue<ReturnParser> ReturnParserPool = new Queue<ReturnParser>();
        public static Queue<FunctionAParser> FunctionAParserPool = new Queue<FunctionAParser>();
        public static Queue<FunctionANParser> FunctionANParserPool = new Queue<FunctionANParser>();
        public static Queue<FunctionParser> FunctionParserPool = new Queue<FunctionParser>();
        public static Queue<FunctionNParser> FunctionNParserPool = new Queue<FunctionNParser>();
        public static Queue<IfParser> IfParserPool = new Queue<IfParser>();
        public static Queue<IfElseParser> IfElseParserPool = new Queue<IfElseParser>();
        public static Queue<ParenParser> ParenParserPool = new Queue<ParenParser>();
        public static Queue<PropertyParser> PropertyParserPool = new Queue<PropertyParser>();
        public static Queue<IndexParser> IndexParserPool = new Queue<IndexParser>();
        public static Queue<CallParser> CallParserPool = new Queue<CallParser>();
        public static Queue<CallNParser> CallNParserPool = new Queue<CallNParser>();
        public static Queue<ConcatParser> ConcatParserPool = new Queue<ConcatParser>();
        public static Queue<NegateParser> NegateParserPool = new Queue<NegateParser>();
        public static Queue<NotParser> NotParserPool = new Queue<NotParser>();
        public static Queue<MultiplyParser> MultiplyParserPool = new Queue<MultiplyParser>();
        public static Queue<DivisionParser> DivisionParserPool = new Queue<DivisionParser>();
        public static Queue<ModParser> ModParserPool = new Queue<ModParser>();
        public static Queue<PlusParser> PlusParserPool = new Queue<PlusParser>();
        public static Queue<SubtractParser> SubtractParserPool = new Queue<SubtractParser>();
        public static Queue<LessParser> LessParserPool = new Queue<LessParser>();
        public static Queue<GreaterParser> GreaterParserPool = new Queue<GreaterParser>();
        public static Queue<LessEqualParser> LessEqualParserPool = new Queue<LessEqualParser>();
        public static Queue<GreaterEqualParser> GreaterEqualParserPool = new Queue<GreaterEqualParser>();
        public static Queue<EqualParser> EqualParserPool = new Queue<EqualParser>();
        public static Queue<NotEqualParser> NotEqualParserPool = new Queue<NotEqualParser>();
        public static Queue<AndParser> AndParserPool = new Queue<AndParser>();
        public static Queue<OrParser> OrParserPool = new Queue<OrParser>();
        public static Queue<ListParser> ListParserPool = new Queue<ListParser>();
        public static Queue<ListNParser> ListNParserPool = new Queue<ListNParser>();
        public static Queue<TableNParser> TableNParserPool = new Queue<TableNParser>();
        public static Queue<TableN2Parser> TableN2ParserPool = new Queue<TableN2Parser>();

        public static ModuleParser GetModuleParser() {
            if (ModuleParserPool.Count == 0) {
                return new ModuleParser();
            } else {
                return ModuleParserPool.Dequeue();
            }
        }

        public static BreakParser GetBreakParser() {
            if (BreakParserPool.Count == 0) {
                return new BreakParser();
            } else {
                return BreakParserPool.Dequeue();
            }
        }

        public static WhileParser GetWhileParser() {
            if (WhileParserPool.Count == 0) {
                return new WhileParser();
            } else {
                return WhileParserPool.Dequeue();
            }
        }

        public static ForNParser GetForNParser() {
            if (ForNParserPool.Count == 0) {
                return new ForNParser();
            } else {
                return ForNParserPool.Dequeue();
            }
        }

        public static ForParser GetForParser() {
            if (ForParserPool.Count == 0) {
                return new ForParser();
            } else {
                return ForParserPool.Dequeue();
            }
        }

        public static ForEachParser GetForEachParser() {
            if (ForEachParserPool.Count == 0) {
                return new ForEachParser();
            } else {
                return ForEachParserPool.Dequeue();
            }
        }

        public static DefineParser GetDefineParser() {
            if (DefineParserPool.Count == 0) {
                return new DefineParser();
            } else {
                return DefineParserPool.Dequeue();
            }
        }

        public static DefineNParser GetDefineNParser() {
            if (DefineNParserPool.Count == 0) {
                return new DefineNParser();
            } else {
                return DefineNParserPool.Dequeue();
            }
        }

        public static BindParser GetBindParser() {
            if (BindParserPool.Count == 0) {
                return new BindParser();
            } else {
                return BindParserPool.Dequeue();
            }
        }

        public static BindNParser GetBindNParser() {
            if (BindNParserPool.Count == 0) {
                return new BindNParser();
            } else {
                return BindNParserPool.Dequeue();
            }
        }

        public static ReturnNParser GetReturnNParser() {
            if (ReturnNParserPool.Count == 0) {
                return new ReturnNParser();
            } else {
                return ReturnNParserPool.Dequeue();
            }
        }

        public static ReturnParser GetReturnParser() {
            if (ReturnParserPool.Count == 0) {
                return new ReturnParser();
            } else {
                return ReturnParserPool.Dequeue();
            }
        }

        public static FunctionAParser GetFunctionAParser() {
            if (FunctionAParserPool.Count == 0) {
                return new FunctionAParser();
            } else {
                return FunctionAParserPool.Dequeue();
            }
        }

        public static FunctionANParser GetFunctionANParser() {
            if (FunctionANParserPool.Count == 0) {
                return new FunctionANParser();
            } else {
                return FunctionANParserPool.Dequeue();
            }
        }

        public static FunctionParser GetFunctionParser() {
            if (FunctionParserPool.Count == 0) {
                return new FunctionParser();
            } else {
                return FunctionParserPool.Dequeue();
            }
        }

        public static FunctionNParser GetFunctionNParser() {
            if (FunctionNParserPool.Count == 0) {
                return new FunctionNParser();
            } else {
                return FunctionNParserPool.Dequeue();
            }
        }

        public static IfParser GetIfParser() {
            if (IfParserPool.Count == 0) {
                return new IfParser();
            } else {
                return IfParserPool.Dequeue();
            }
        }

        public static IfElseParser GetIfElseParser() {
            if (IfElseParserPool.Count == 0) {
                return new IfElseParser();
            } else {
                return IfElseParserPool.Dequeue();
            }
        }

        public static ParenParser GetParenParser() {
            if (ParenParserPool.Count == 0) {
                return new ParenParser();
            } else {
                return ParenParserPool.Dequeue();
            }
        }

        public static PropertyParser GetPropertyParser() {
            if (PropertyParserPool.Count == 0) {
                return new PropertyParser();
            } else {
                return PropertyParserPool.Dequeue();
            }
        }

        public static IndexParser GetIndexParser() {
            if (IndexParserPool.Count == 0) {
                return new IndexParser();
            } else {
                return IndexParserPool.Dequeue();
            }
        }

        public static CallParser GetCallParser() {
            if (CallParserPool.Count == 0) {
                return new CallParser();
            } else {
                return CallParserPool.Dequeue();
            }
        }

        public static CallNParser GetCallNParser() {
            if (CallNParserPool.Count == 0) {
                return new CallNParser();
            } else {
                return CallNParserPool.Dequeue();
            }
        }

        public static ConcatParser GetConcatParser() {
            if (ConcatParserPool.Count == 0) {
                return new ConcatParser();
            } else {
                return ConcatParserPool.Dequeue();
            }
        }

        public static NegateParser GetNegateParser() {
            if (NegateParserPool.Count == 0) {
                return new NegateParser();
            } else {
                return NegateParserPool.Dequeue();
            }
        }

        public static NotParser GetNotParser() {
            if (NotParserPool.Count == 0) {
                return new NotParser();
            } else {
                return NotParserPool.Dequeue();
            }
        }

        public static MultiplyParser GetMultiplyParser() {
            if (MultiplyParserPool.Count == 0) {
                return new MultiplyParser();
            } else {
                return MultiplyParserPool.Dequeue();
            }
        }

        public static DivisionParser GetDivisionParser() {
            if (DivisionParserPool.Count == 0) {
                return new DivisionParser();
            } else {
                return DivisionParserPool.Dequeue();
            }
        }

        public static ModParser GetModParser() {
            if (ModParserPool.Count == 0) {
                return new ModParser();
            } else {
                return ModParserPool.Dequeue();
            }
        }

        public static PlusParser GetPlusParser() {
            if (PlusParserPool.Count == 0) {
                return new PlusParser();
            } else {
                return PlusParserPool.Dequeue();
            }
        }

        public static SubtractParser GetSubtractParser() {
            if (SubtractParserPool.Count == 0) {
                return new SubtractParser();
            } else {
                return SubtractParserPool.Dequeue();
            }
        }

        public static LessParser GetLessParser() {
            if (LessParserPool.Count == 0) {
                return new LessParser();
            } else {
                return LessParserPool.Dequeue();
            }
        }

        public static GreaterParser GetGreaterParser() {
            if (GreaterParserPool.Count == 0) {
                return new GreaterParser();
            } else {
                return GreaterParserPool.Dequeue();
            }
        }

        public static LessEqualParser GetLessEqualParser() {
            if (LessEqualParserPool.Count == 0) {
                return new LessEqualParser();
            } else {
                return LessEqualParserPool.Dequeue();
            }
        }

        public static GreaterEqualParser GetGreaterEqualParser() {
            if (GreaterEqualParserPool.Count == 0) {
                return new GreaterEqualParser();
            } else {
                return GreaterEqualParserPool.Dequeue();
            }
        }

        public static EqualParser GetEqualParser() {
            if (EqualParserPool.Count == 0) {
                return new EqualParser();
            } else {
                return EqualParserPool.Dequeue();
            }
        }

        public static NotEqualParser GetNotEqualParser() {
            if (NotEqualParserPool.Count == 0) {
                return new NotEqualParser();
            } else {
                return NotEqualParserPool.Dequeue();
            }
        }

        public static AndParser GetAndParser() {
            if (AndParserPool.Count == 0) {
                return new AndParser();
            } else {
                return AndParserPool.Dequeue();
            }
        }

        public static OrParser GetOrParser() {
            if (OrParserPool.Count == 0) {
                return new OrParser();
            } else {
                return OrParserPool.Dequeue();
            }
        }

        public static ListParser GetListParser() {
            if (ListParserPool.Count == 0) {
                return new ListParser();
            } else {
                return ListParserPool.Dequeue();
            }
        }

        public static ListNParser GetListNParser() {
            if (ListNParserPool.Count == 0) {
                return new ListNParser();
            } else {
                return ListNParserPool.Dequeue();
            }
        }

        public static TableNParser GetTableNParser() {
            if (TableNParserPool.Count == 0) {
                return new TableNParser();
            } else {
                return TableNParserPool.Dequeue();
            }
        }

        public static TableN2Parser GetTableN2Parser() {
            if (TableN2ParserPool.Count == 0) {
                return new TableN2Parser();
            } else {
                return TableN2ParserPool.Dequeue();
            }
        }


        public static void PutModuleParser(IParser parser) {
            ModuleParserPool.Enqueue((ModuleParser)parser);
        }

        public static void PutBreakParser(IParser parser) {
            BreakParserPool.Enqueue((BreakParser)parser);
        }

        public static void PutWhileParser(IParser parser) {
            WhileParserPool.Enqueue((WhileParser)parser);
        }

        public static void PutForNParser(IParser parser) {
            ForNParserPool.Enqueue((ForNParser)parser);
        }

        public static void PutForParser(IParser parser) {
            ForParserPool.Enqueue((ForParser)parser);
        }

        public static void PutForEachParser(IParser parser) {
            ForEachParserPool.Enqueue((ForEachParser)parser);
        }

        public static void PutDefineParser(IParser parser) {
            DefineParserPool.Enqueue((DefineParser)parser);
        }

        public static void PutDefineNParser(IParser parser) {
            DefineNParserPool.Enqueue((DefineNParser)parser);
        }

        public static void PutBindParser(IParser parser) {
            BindParserPool.Enqueue((BindParser)parser);
        }

        public static void PutBindNParser(IParser parser) {
            BindNParserPool.Enqueue((BindNParser)parser);
        }

        public static void PutReturnNParser(IParser parser) {
            ReturnNParserPool.Enqueue((ReturnNParser)parser);
        }

        public static void PutReturnParser(IParser parser) {
            ReturnParserPool.Enqueue((ReturnParser)parser);
        }

        public static void PutFunctionAParser(IParser parser) {
            FunctionAParserPool.Enqueue((FunctionAParser)parser);
        }

        public static void PutFunctionANParser(IParser parser) {
            FunctionANParserPool.Enqueue((FunctionANParser)parser);
        }

        public static void PutFunctionParser(IParser parser) {
            FunctionParserPool.Enqueue((FunctionParser)parser);
        }

        public static void PutFunctionNParser(IParser parser) {
            FunctionNParserPool.Enqueue((FunctionNParser)parser);
        }

        public static void PutIfParser(IParser parser) {
            IfParserPool.Enqueue((IfParser)parser);
        }

        public static void PutIfElseParser(IParser parser) {
            IfElseParserPool.Enqueue((IfElseParser)parser);
        }

        public static void PutParenParser(IParser parser) {
            ParenParserPool.Enqueue((ParenParser)parser);
        }

        public static void PutPropertyParser(IParser parser) {
            PropertyParserPool.Enqueue((PropertyParser)parser);
        }

        public static void PutIndexParser(IParser parser) {
            IndexParserPool.Enqueue((IndexParser)parser);
        }

        public static void PutCallParser(IParser parser) {
            CallParserPool.Enqueue((CallParser)parser);
        }

        public static void PutCallNParser(IParser parser) {
            CallNParserPool.Enqueue((CallNParser)parser);
        }

        public static void PutConcatParser(IParser parser) {
            ConcatParserPool.Enqueue((ConcatParser)parser);
        }

        public static void PutNegateParser(IParser parser) {
            NegateParserPool.Enqueue((NegateParser)parser);
        }

        public static void PutNotParser(IParser parser) {
            NotParserPool.Enqueue((NotParser)parser);
        }

        public static void PutMultiplyParser(IParser parser) {
            MultiplyParserPool.Enqueue((MultiplyParser)parser);
        }

        public static void PutDivisionParser(IParser parser) {
            DivisionParserPool.Enqueue((DivisionParser)parser);
        }

        public static void PutModParser(IParser parser) {
            ModParserPool.Enqueue((ModParser)parser);
        }

        public static void PutPlusParser(IParser parser) {
            PlusParserPool.Enqueue((PlusParser)parser);
        }

        public static void PutSubtractParser(IParser parser) {
            SubtractParserPool.Enqueue((SubtractParser)parser);
        }

        public static void PutLessParser(IParser parser) {
            LessParserPool.Enqueue((LessParser)parser);
        }

        public static void PutGreaterParser(IParser parser) {
            GreaterParserPool.Enqueue((GreaterParser)parser);
        }

        public static void PutLessEqualParser(IParser parser) {
            LessEqualParserPool.Enqueue((LessEqualParser)parser);
        }

        public static void PutGreaterEqualParser(IParser parser) {
            GreaterEqualParserPool.Enqueue((GreaterEqualParser)parser);
        }

        public static void PutEqualParser(IParser parser) {
            EqualParserPool.Enqueue((EqualParser)parser);
        }

        public static void PutNotEqualParser(IParser parser) {
            NotEqualParserPool.Enqueue((NotEqualParser)parser);
        }

        public static void PutAndParser(IParser parser) {
            AndParserPool.Enqueue((AndParser)parser);
        }

        public static void PutOrParser(IParser parser) {
            OrParserPool.Enqueue((OrParser)parser);
        }

        public static void PutListParser(IParser parser) {
            ListParserPool.Enqueue((ListParser)parser);
        }

        public static void PutListNParser(IParser parser) {
            ListNParserPool.Enqueue((ListNParser)parser);
        }

        public static void PutTableNParser(IParser parser) {
            TableNParserPool.Enqueue((TableNParser)parser);
        }

        public static void PutTableN2Parser(IParser parser) {
            TableN2ParserPool.Enqueue((TableN2Parser)parser);
        }


    }

}