using System;
using System.Collections.Generic;

namespace Definux.HtmlBuilder
{
    public class HtmlElementsCollection : List<HtmlElement>
    {
        internal HtmlElementsCollection()
        {
        }

        /// <summary>
        /// Insert HTML element into current collection.
        /// </summary>
        /// <param name="elementAction"></param>
        /// <returns></returns>
        public HtmlElementsCollection Append(Action<HtmlElement> elementAction)
        {
            var element = new HtmlElement();
            elementAction.Invoke(element);
            this.Add(element);
            return this;
        }
    }
}
