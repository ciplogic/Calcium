using System.IO;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class VariableResolved : ExprResolverBase
    {
        private readonly VariableDefinition _variable;

        public VariableResolved(VariableDefinition variable) 
            : base(ExpressionKind.Variable)
        {
            if(variable==null)
                throw new InvalidDataException();
            _variable = variable;
            
        }

        public override string ToCode()
        {
            return _variable==null? "UNKNOWN": _variable.Name;
        }

        public override bool CalculateExpressionType()
        {
            if (_variable.Type == null)
                return false;
            ExpressionType = _variable.Type;
            return true;
        }
    }
}