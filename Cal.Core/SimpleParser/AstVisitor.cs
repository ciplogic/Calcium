namespace Cal.Core.SimpleParser
{
    public abstract class AstVisitor
    {
        public abstract void Visit(AstNode node);
    }
}