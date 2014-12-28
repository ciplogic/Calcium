﻿namespace Cal.Core.Definitions.ExpressionResolvers
{
    internal class VariableResolved : ExprResolverBase
    {
        private readonly VariableDefinition _variable;

        public VariableResolved(VariableDefinition variable) 
            : base(ExpressionKind.Variable)
        {
            _variable = variable;
        }

        public override string ToCode()
        {

            return _variable==null? "UNKNOWN": _variable.Name;
        }
    }
}