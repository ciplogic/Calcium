namespace Cal.Core.Definitions
{
    public class VariableDefinition
    {
        public string Name { get; set; }
        public ClassDefinition Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Type.Name);
        }
    }
}