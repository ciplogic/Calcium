namespace Cal.Core.Definitions
{
    public class BlockDefinition : BaseDefinition
    {
        public ScopeDefinition Scope { get; set; }
        public BlockDefinition()
        {
            Scope = new ScopeDefinition();
        }
    }
}