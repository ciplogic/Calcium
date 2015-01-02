namespace Cal.Core.Definitions
{
    public class BlockDefinition : BaseDefinition
    {
        public ScopeDefinition Scope { get; set; }
        public BlockKind Kind { get; set; }
        public BlockDefinition(ScopeDefinition scope, string name, BlockKind kind)
        {
            Kind = kind;
            Scope = new ScopeDefinition(scope, name);
        }
    }
}