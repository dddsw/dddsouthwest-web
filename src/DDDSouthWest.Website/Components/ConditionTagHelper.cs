using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DDDSouthWest.Website.Components
{
    [HtmlTargetElement("div")]
    public class VisibilityTagHelper : TagHelper
    {
        public bool IsVisible { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!IsVisible)
                output.SuppressOutput();

            base.Process(context, output);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!IsVisible)
                output.SuppressOutput();

            return base.ProcessAsync(context, output);
        }
    }
}