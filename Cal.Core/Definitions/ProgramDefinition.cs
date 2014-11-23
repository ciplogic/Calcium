using System.Collections.Generic;

namespace Cal.Core.Definitions
{
    public class ProgramDefinition : BaseDefinition
    {
        public List<ClassDefinition> Classes { get; set; }
        public ClassDefinition GlobalClass;

        public ProgramDefinition()
        {
            Classes = new List<ClassDefinition>();

            GlobalClass = new ClassDefinition
            {
                Name = "_Global",
                Namespace = "Cal"
            };
            Classes.Add(GlobalClass);
            var methodDefinition = new MethodDefinition()
            {
                Name = "Main",
                IsStatic = true
            
            };
            GlobalClass.AddMethodToClass(methodDefinition);
        }
    }
}
