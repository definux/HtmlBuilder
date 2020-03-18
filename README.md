# Definux.HtmlBuilder
Lightweight HTML5 builder for creating HTML string with fluent method syntax of C#.

## Basic Examples

```
HtmlBuilder builder = new HtmlBuilder();
builder.StartElement(HtmlTags.Input)
            .WithAttribute("type", "email")
            .WithAttribute("name", "email")
            .WithAttribute("placeholder", "Enter your email");

string actualHtml = builder.RenderHtml();

// Builder result
// <input type="email" name="email" placeholder="Enter your email"/>
```


