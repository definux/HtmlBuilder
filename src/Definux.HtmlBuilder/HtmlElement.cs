using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Definux.HtmlBuilder
{
    public class HtmlElement
    {
        private HtmlTag tag;
        private IList<HtmlElementAttribute> attributes;
        private string rawText;
        private bool hasRawText;
        private HtmlElementsCollection children;

        internal HtmlElement()
        {
            this.attributes = new List<HtmlElementAttribute>();
            this.children = new HtmlElementsCollection();
        }

        internal HtmlElement(string rawText)
        {
            this.rawText = rawText;
            this.hasRawText = true;
            this.attributes = new List<HtmlElementAttribute>();
            this.children = new HtmlElementsCollection();
        }

        internal HtmlElement(HtmlTag tag)
        {
            this.tag = tag;
            this.attributes = new List<HtmlElementAttribute>();
            this.children = new HtmlElementsCollection();
        }

        public HtmlTag Tag => this.tag;

        public IList<HtmlElementAttribute> Attributes => this.attributes;

        private string TagLayout
        {
            get
            {
                if (this.hasRawText)
                {
                    return string.Empty;
                }
                return this.tag.HasClosingTag ? Layouts.StartEndTagLayout : Layouts.SingleTagLayout;
            }
        }

        /// <summary>
        /// Start HTML element definition if the element type is not defined.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public HtmlElement OpenElement(HtmlTag tag)
        {
            this.tag = tag;
            return this;
        }

        /// <summary>
        /// Insert HTML element as a child of caller element.
        /// </summary>
        /// <param name="elementAction"></param>
        /// <returns></returns>
        public HtmlElement Append(Action<HtmlElement> elementAction)
        {
            var childElement = new HtmlElement();
            elementAction.Invoke(childElement);
            this.children.Add(childElement);
            return this;
        }

        /// <summary>
        /// Insert HTML element collection as children of caller element.
        /// </summary>
        /// <param name="elementsAction"></param>
        /// <returns></returns>
        public HtmlElement AppendMultiple(Action<HtmlElementsCollection> elementsAction)
        {
            var childElements = new HtmlElementsCollection();
            elementsAction.Invoke(childElements);
            this.children.AddRange(childElements);
            return this;
        }

        /// <summary>
        /// Insert raw text as a child of caller element.
        /// </summary>
        /// <param name="rawText"></param>
        /// <returns></returns>
        public HtmlElement Append(string rawText)
        {
            var childElement = new HtmlElement(rawText);
            this.children.Add(childElement);
            return this;
        }

        /// <summary>
        /// Set attribute to the HTML element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement WithAttribute(string name, string value)
        {
            if (this.attributes.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Attribute {name} cannot be duplicated!");
            }

            this.attributes.Add(new HtmlElementAttribute(name, value));
            return this;
        }

        /// <summary>
        /// Set attribute to the HTML element on condition.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement WithAttributeIf(string name, string value, bool condition)
        {
            if (this.attributes.Any(x => x.Name == name))
            {
                throw new InvalidOperationException($"Attribute {name} cannot be duplicated!");
            }

            if (condition)
            {
                this.attributes.Add(new HtmlElementAttribute(name, value));
            }
            
            return this;
        }

        /// <summary>
        /// Set classes to the HTML element on condition.
        /// </summary>
        /// <param name="classesOnTrue"></param>
        /// <param name="classesOnFalse"></param>
        /// <param name="condition"></param>
        /// <param name="sharedClasses"></param>
        /// <returns></returns>
        public HtmlElement WithConditionalClasses(string classesOnTrue, string classesOnFalse, bool condition, string sharedClasses = "")
        {
            string sharedClassesSeparationSpace = string.IsNullOrWhiteSpace(sharedClasses) ? string.Empty : " ";

            classesOnTrue = classesOnTrue ?? string.Empty;
            classesOnFalse = classesOnFalse ?? string.Empty;

            WithClasses($"{(condition ? classesOnTrue.Trim() : classesOnFalse.Trim())}{sharedClassesSeparationSpace}{sharedClasses.Trim()}");

            return this;
        }

        /// <summary>
        /// Set HTML element Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlElement WithId(string id)
        {
            return WithAttribute("id", id);
        }

        /// <summary>
        /// Set HTML classes.
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        public HtmlElement WithClasses(string classes)
        {
            return WithAttribute("class", classes);
        }

        /// <summary>
        /// Set custom data attribute to HTML element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement WithDataAttribute(string name, string value)
        {
            return WithAttribute($"data-{name}", value);
        }

        internal string GetElementContent()
        {
            StringBuilder content = new StringBuilder();
            foreach (var child in this.children)
            {
                content.Append(child.GetElementHtml());
            }

            return content.ToString();
        }

        internal string GetElementHtml()
        {
            if (!this.hasRawText)
            {
                string elementLayout = string.Format(TagLayout, this.tag.Name, string.Join("", this.attributes));

                return string.Format(elementLayout, GetElementContent());
            }

            return this.rawText;
        }

        public override string ToString()
        {
            if (!this.hasRawText)
            {
                return string.Format(TagLayout, this.tag.Name, string.Join("", this.attributes));
            }

            return this.rawText;
        }
    }
}
