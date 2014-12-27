namespace Cal.Core.Definitions
{
    public class VariableDefinition
    {
        public string Name { get; set; }
        public ClassDefinition Type { get; set; }

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