using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class IfDefinition : InstructionDefinition
    {
        private readonly AstNode _item;
        public ScopeDefinition IfBody { get; set; }
        public ScopeDefinition ElseBody { get; set; }

        public IfDefinition(AstNode item)
        {
            _item = item;
            IfBody = new ScopeDefinition();
            ElseBody = new ScopeDefinition();
        }
    }
}