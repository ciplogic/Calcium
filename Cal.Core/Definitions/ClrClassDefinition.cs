using System;

namespace Cal.Core.Definitions
{
    public class ClrClassDefinition : ClassDefinition
    {
        private readonly Type _type;

        public ClrClassDefinition(Type type)
        {
            _type = type;
            Name = _type.Name;
            Namespace = _type.Namespace;
        }
    }
}