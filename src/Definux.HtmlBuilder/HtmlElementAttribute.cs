namespace Definux.HtmlBuilder
{
    public class HtmlElementAttribute
    {
        internal HtmlElementAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        internal string Name { get; private set; }

        internal string Value { get; private set; }

        public override string ToString()
        {
            return string.Format(Layouts.AttributeLayout, Name, Value);
        }
    }
}
