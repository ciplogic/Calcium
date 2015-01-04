using Cal.Core.Definitions.IdentifierDefinition;

namespace Cal.Core.Definitions.ReferenceDefinitions
{
    public class ReferenceVariableDefinition : ReferenceValueDefinition
    {
        private readonly VariableDefinition _variableDefinition;

        public ReferenceVariableDefinition(VariableDefinition variableDefinition) : 
            base(ReferenceValueKind.Variable)
        {
            _variableDefinition = variableDefinition;
            Definition = variableDefinition;
        }

        public VariableDefinition VariableDefinition
        {
            get { return _variableDefinition; }
        }
    }
}