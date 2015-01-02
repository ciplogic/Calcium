using System.Collections.Generic;

namespace Cal.Core.Definitions
{
    public class ClassDefinition : BlockDefinition
    {
        public ClassDefinition(ProgramDefinition programScope)
            : base(programScope.Scope, "Class", BlockKind.Class)
        {
            Defs = new List<MethodDefinition>();
            Interfaces = new List<ClassDefinition>();
        }

        public bool IsClrType { get; set; }
        public List<MethodDefinition> Defs { get; private set; } 
        public string Namespace { get; set; }
        public string Name { get; set; }
        public ClassDefinition BaseType { get; set; }
        public List<ClassDefinition> Interfaces { get; set; }

        public void AddMethodToClass(MethodDefinition methodDefinition)
        {
            Defs.Add(methodDefinition);
            methodDefinition.DeclaringType = this;
        }

        public override string ToString()
        {
            return
                BaseType != null
                    ? string.Format("class {0} < {1}", Name, BaseType.Name)
                    : string.Format("class {0}", Name);
        }
    }
}