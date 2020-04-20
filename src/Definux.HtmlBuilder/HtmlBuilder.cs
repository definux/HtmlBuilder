using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

        public TagHelperOutput ApplyToTagHelperOutput(TagHelperOutput output)
        {
            output.TagName = this.element.Tag.Name;
            output.TagMode = this.element.Tag.HasClosingTag ? TagMode.StartTagAndEndTag : TagMode.SelfClosing;
            foreach (var attribute in this.element.Attributes)
            {
                output.Attributes.Add(new TagHelperAttribute(attribute.Name, attribute.Value));
            }

            output.Content.SetHtmlContent(new HtmlString(this.element.GetElementContent()));

            return output;
        }

        public void Reset()
        {
            this.element = null;
        }
    }
}
