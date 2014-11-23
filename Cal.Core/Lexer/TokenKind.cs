namespace Cal.Core.Lexer
{
    public enum TokenKind
    {
    	None,
        Eoln,
        Eof,
        Comment,
        Spaces,

        OpAssign,
        OpComma,

        OpOpenParen,
        OpCloseParen,
        
        OpOpenSquareParen,
        OpCloseSquareParen,

        OpDot,
        OpOutStream,
        OpResolution,
        OpSingleQuote,
        
        RwClass,
        RwDef,
        RwLoadAssembly,
        RwModule,
        RwNew,
        RwSelf,
        RwEnd,
        RwIf,
        RwElse,
        RwDo,

        Identifier,
        
        PublicIdentifier,

        RegexValue,
        ParenRegexValue,

        OpMul,
        OpAdd,
        OpSub,
        OpDiv,
        OpPipe,
        OpCloseCurlyParen,
        OpOpenCurlyParen,
        OpGreaterThan,
        OpLessThan,
        OpIf,
        OpLessStrict,
        OpGreaterStrict,
        OpSemiColon,
        OpNotEqual,
        OpBoolAnd,
        OpBoolOr,
        OpBoolNot,
        RwWhile,
        Double,
        Integer,
        OpElse,
        OpAddBy,
        OpSubBy,
        OpEquals,
        OpColumn,
        NonTerminal,
        Terminal
    }
}