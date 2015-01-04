namespace Cal.Core.Definitions
{
    public enum VariableKind
    {
        Local,
        Argument,
        Field
    }
    public class VariableDefinition
    {
        public VariableKind Kind { get; set; }
        public string Name { get; set; }
        public ClassDefinition Type { get; set; }

        public VariableDefinition()
        {
            Kind = VariableKind.Local;
        }
        public override string ToString()
        {
            if (Type == null)
            {
                return string.Format("{0}: Unknown", Name);
            }
            return string.Format("{0}: {1}", Name, Type.Name);
        }
    }
}