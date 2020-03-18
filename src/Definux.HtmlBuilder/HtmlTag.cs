namespace Definux.HtmlBuilder
{
    public class HtmlTag
    {
        public HtmlTag()
        { }

        public HtmlTag(string name, bool hasClosingTag = true)
        {
            Name = name;
            HasClosingTag = hasClosingTag;
        }

        public string Name { get; set; }

        public bool HasClosingTag { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator string(HtmlTag tag)
        {
            return tag.Name;
        }
    }
}
