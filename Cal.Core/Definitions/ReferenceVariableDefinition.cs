using Cal.Core.Definitions.IdentifierDefinition;

namespace Cal.Core.Definitions
{
    public class ReferenceVariableDefinition : ReferenceValueDefinition
    {
        private readonly VariableDefinition _variableDefinition;

        public ReferenceVariableDefinition(VariableDefinition variableDefinition) : 
            base(ReferenceValueKind.Variable)
        {
            _variableDefinition = variableDefinition;
        }

        public VariableDefinition VariableDefinition
        {
            get { return _variableDefinition; }
        }
    }
}