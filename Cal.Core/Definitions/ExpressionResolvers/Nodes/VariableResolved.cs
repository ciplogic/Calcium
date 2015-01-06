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

        public VariableDefinition Variable
        {
            get { return _variable; }
        }

        public override string ToCode()
        {
            return Variable==null? "UNKNOWN": Variable.Name;
        }

        public override bool CalculateExpressionType()
        {
            if (Variable.Type == null)
                return false;
            ExpressionType = Variable.Type;
            return true;
        }
    }
}