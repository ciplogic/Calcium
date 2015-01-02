using System.Reflection;

namespace Cal.Core.Definitions.IdentifierDefinition
{
    public class ReferenceValueDefinition
    {
        public ReferenceValueKind Kind { get; set; }
        public object Definition { get; set; }

        public ReferenceValueDefinition(ReferenceValueKind kind)
        {
            Kind = kind;
        }
    }

    public class MethodReferenceDefinition : ReferenceValueDefinition
    {
        private readonly MethodInfo _method;

        public MethodReferenceDefinition(MethodInfo method) : base(ReferenceValueKind.Method)
        {
            _method = method;
        }
    }
}