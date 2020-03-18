using System.Collections.Generic;
using NUnit.Framework;

namespace Definux.HtmlBuilder.Tests
{
    public class UnitTestsForms
    {
        protected HtmlBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new HtmlBuilder();
        }

        [Test]
        public void FormInputTypeEmail_IsHtmlCorrect_True()
        {
            string expectedHtml = "<input type=\"email\" name=\"email\" placeholder=\"Enter your email\"/>";

            builder.StartElement(HtmlTags.Input)
                .WithAttribute("type", "email")
                .WithAttribute("name", "email")
                .WithAttribute("placeholder", "Enter your email");

            string actualHtml = builder.RenderHtml();
            Assert.AreEqual(expectedHtml, actualHtml);
            builder.Reset();
        }

        [Test]
        public void SelectWithOptions_IsHtmlCorrect_True()
        {
            string expectedHtml =
                "<select name=\"food\">" +
                "<option value=\"1\">Burger</option>" +
                "<option value=\"2\">Pizza</option>" +
                "<option value=\"3\">Ice Cream</option>" +
                "</select>";

            var selectOptions = new Dictionary<string, string>
            {
                { "1", "Burger" },
                { "2", "Pizza" },
                { "3", "Ice Cream" }
            };

            var htmlElement= builder.StartElement(HtmlTags.Select)
                .WithAttribute("name", "food")
                .AppendMultiple(x =>
                {
                    foreach (var option in selectOptions)
                    {
                        x.Append(xx => xx
                            .OpenElement(HtmlTags.Option)
                            .WithAttribute("value", option.Key)
                            .Append(option.Value)
                        );
                    }
                });

            string actualHtml = builder.RenderHtml();
            Assert.AreEqual(expectedHtml, actualHtml);
            builder.Reset();
        }
    }
}