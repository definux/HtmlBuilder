# Definux.HtmlBuilder
Lightweight HTML5 builder for creating HTML string with fluent method syntax of C#.

## Basic Examples

#### Single element
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

#### Complex element
```
var selectOptions = new Dictionary<string, string>
{
    { "1", "Burger" },
    { "2", "Pizza" },
    { "3", "Ice Cream" }
};

HtmlBuilder builder = new HtmlBuilder();
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

// Builder result
// <input type="email" name="email" placeholder="Enter your email"/>
```

