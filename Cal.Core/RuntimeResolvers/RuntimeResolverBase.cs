
using System;
using System.Collections.Generic;
using Cal.Core.Definitions;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;

namespace Cal.Core.RuntimeResolvers
{
	/// <summary>
	/// Description of RuntimeResolverBase.
	/// </summary>
	public abstract class RuntimeResolverBase
	{
	    public abstract bool CanResolveFunction(List<TokenDef> tokens, BlockDefinition instructionDefinition);

	    public abstract ExprResolverBase FunctionResolve(List<TokenDef> contentTokens,
	        BlockDefinition instructionDefinition);
	}
}
