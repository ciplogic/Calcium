namespace Cal.Core.Definitions.ExpressionResolvers
{
    internal class VariableResolved : ExprResolverBase
    {
        private readonly VariableDefinition _variable;

        public VariableResolved(VariableDefinition variable)
        {
            _variable = variable;
        }
    }
}