using System;

namespace Definux.HtmlBuilder
{
    public class HtmlBuilder : IHtmlBuilder
    {
        private HtmlElement element;

        public HtmlElement StartElement(HtmlTag tag)
        {
            this.element = new HtmlElement(tag);

            return this.element;
        }

        public string RenderHtml()
        {
            if (this.element == null)
            {
                throw new ArgumentNullException("HTML Element", "HTML element is not build. Use StartElement method to build your HTML element");
            }

            return element.GetElementHtml();
        }

        public void Reset()
        {
            this.element = null;
        }
    }
}
