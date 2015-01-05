namespace Cal.Core.Definitions.ExpressionResolvers
{
    public enum ExpressionKind
    {
        Unknown,
        Constant,
        Variable,
        FunctionCall,
        Parentheses,
        UnaryOperator,
        BinaryOperator,
        SpecialOperation,
        MultiToken
    }
}