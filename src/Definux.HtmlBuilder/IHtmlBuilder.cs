using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Definux.HtmlBuilder
{
    /// <summary>
    /// Main HTML builder.
    /// </summary>
    public interface IHtmlBuilder
    {
        /// <summary>
        /// Start fluent building HTML element.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        HtmlElement StartElement(HtmlTag tag);

        /// <summary>
        /// Render HTML content based on built element.
        /// </summary>
        /// <returns></returns>
        string RenderHtml();

        /// <summary>
        /// Reset builder element.
        /// </summary>
        void Reset();

        /// <summary>
        /// Apply current HTML builder result to a <see cref="TagHelperOutput"/>.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        TagHelperOutput ApplyToTagHelperOutput(TagHelperOutput output);
    }
}
