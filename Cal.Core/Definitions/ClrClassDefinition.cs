using System;

namespace Cal.Core.Definitions
{
    public class ClrClassDefinition : ClassDefinition
    {
        private readonly Type _type;

        public ClrClassDefinition(Type type)
            : base(ProgramDefinition.Instance)
        {
            _type = type;
            Name = _type.Name;
            Namespace = _type.Namespace;
        }
    }
}