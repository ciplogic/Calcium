using Cal.Core.Definitions.IdentifierDefinition;

namespace Cal.Core.Definitions.ReferenceDefinitions
{
    public class ReferenceFunctionDefinition : ReferenceValueDefinition
    {
        public ReferenceFunctionDefinition()
            : base(ReferenceValueKind.Function)
        {
        }

        public ClassDefinition ReturnType { get; set; }
    }
}