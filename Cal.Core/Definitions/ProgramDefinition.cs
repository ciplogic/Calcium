using System.Collections.Generic;

namespace Cal.Core.Definitions
{
    public class ProgramDefinition : BlockDefinition
    {
        public List<ClassDefinition> Classes { get; set; }
        public ClassDefinition GlobalClass;
        
        public ProgramDefinition() : base(null, "Program", BlockKind.Program)
        {
            Classes = new List<ClassDefinition>();

            GlobalClass = new ClassDefinition(this)
            {
                Name = "_Global",
                Namespace = "Cal"
            };
            Classes.Add(GlobalClass);
            var methodDefinition = new MethodDefinition(GlobalClass)
            {
                Name = "Main",
                IsStatic = true
            
            };
            GlobalClass.AddMethodToClass(methodDefinition);
        }
    }
}
