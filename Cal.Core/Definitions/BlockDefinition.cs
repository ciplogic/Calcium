namespace Cal.Core.Definitions
{
    public class BlockDefinition : BaseDefinition
    {
        public ScopeDefinition Scope { get; set; }
        public BlockDefinition(ScopeDefinition scope, string name)
        {
            Scope = new ScopeDefinition(scope, name);
        }
    }
}