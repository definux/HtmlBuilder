using System;
using NUnit.Framework;

namespace Definux.HtmlBuilder.Tests
{
    public class UnitTestsAttributes
    {
        protected HtmlBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new HtmlBuilder();
        }

        [Test]
        public void ElementWithManyAttributes_IsHtmlCorrect_True()
        {
            string expectedHtml =
                "<div " +
                "id=\"test\" " +
                "class=\"container\" " +
                "data-id=\"23\" " +
                "data-order=\"7\" " +
                "style=\"background: #000;\">" +
                "</div>";

            builder.StartElement(HtmlTags.Div)
                .WithId("test")
                .WithClasses("container")
                .WithDataAttribute("id", "23")
                .WithDataAttribute("order", "7")
                .WithAttribute("style", "background: #000;");

            string actualHtml = builder.RenderHtml();
            Assert.AreEqual(expectedHtml, actualHtml);
            builder.Reset();
        }

        [Test]
        public void ElementWithDuplicatedAttribute_InvalidExpression_ExceptionThrown()
        {
            Assert.Throws(Is.TypeOf<InvalidOperationException>(), delegate
            {
                builder.StartElement(HtmlTags.Img)
                    .WithId("logo")
                    .WithId("brand");
            });
        }
    }
}
