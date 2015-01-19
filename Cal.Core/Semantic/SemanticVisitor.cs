using System;
using System.Collections.Generic;
using Cal.Core.Definitions;
using Cal.Core.Definitions.Assigns;
using Cal.Core.Definitions.Instruction;
using Cal.Core.Definitions.ReferenceDefinitions;

namespace Cal.Core.Semantic
{
    public class SemanticVisitor
    {
    	public List<Action<BlockDefinition>> BlockHandlers = 
    		new List<Action<BlockDefinition>>();

        public SemanticVisitor()
        {
            Action<BlockDefinition> evaluateAssigns = HandleAssign;
            BlockHandlers.Add(evaluateAssigns);
        }

        private void HandleAssign(BlockDefinition obj)
        {
            foreach (var op in obj.Scope.Operations)
            {
                var assign = op as AssignDefinition;
                if (assign == null)
                    continue;
                var left = assign.Left.ReferenceDefinition;
                var refVariable = left as ReferenceVariableDefinition;
                if(refVariable==null)
                    continue;
                if(refVariable.VariableDefinition.Type != null)
                    continue;
                if(!assign.RightExpression.CalculateExpressionType())
                    continue;
                refVariable.VariableDefinition.Type = assign.RightExpression.ExpressionType;
            }
        }

        public void Build(ProgramDefinition progDefinition)
        {
        	CallHandlers(progDefinition);
        	foreach(var clazz in progDefinition.Classes)
        	{
        		Evaluate(clazz);
        	}
        }

		void Evaluate(ClassDefinition clazz)
		{
			CallHandlers(clazz);
			foreach(var def in clazz.Defs)
			{
				EvaluateBlock(def);
			}
		}

		void EvaluateBlock(BlockDefinition def)
		{
			CallHandlers(def);
			foreach(var op in def.Scope.Operations)
			{
				var ifOp = op as IfDefinition;
			    if (ifOp != null)
			        EvaluateIf(ifOp);

                var whileOp = op as WhileDefinition;
                if (whileOp != null)
                    EvaluateWhile(whileOp);
			}
		}

        private void EvaluateWhile(WhileDefinition whileOp)
        {
            EvaluateBlock(whileOp.WhileBody);
        }

        private void EvaluateIf(IfDefinition ifOp)
        {
            EvaluateBlock(ifOp.IfBody);
            if (ifOp.ElseBody!=null)
                EvaluateBlock(ifOp.ElseBody);
        }

        public void CallHandlers(BlockDefinition definition)
        {
        	foreach(var handler in BlockHandlers)
        	{
        		handler(definition);
        	}
        }
    }
}
