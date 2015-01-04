using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Definitions.ReferenceDefinitions;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class BlockDefinition : BaseDefinition
    {
        public ScopeDefinition Scope { get; set; }
        public BlockKind Kind { get; set; }

        public BlockDefinition Parent { get; set; }
        public BlockDefinition(BlockDefinition parent, string name, BlockKind kind)
        {
            Parent = parent;
            Kind = kind;
            Scope = new ScopeDefinition(name);
        }

        public virtual ReferenceValueDefinition LocateVariable(TokenDef tokenDef)
        {
            var variableDefinition = Scope.LocateVariable(tokenDef.GetContent());
            if (variableDefinition == null && Parent != null)
                return Parent.LocateVariable(tokenDef);
            return new ReferenceVariableDefinition(variableDefinition);
        }

        public void AddVariable(TokenDef tokenDef)
        {
            Scope.AddVariable(tokenDef.GetContent());
        }
    }
}