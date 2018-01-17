using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DDDSouthWest.Website.Components
{
    [HtmlTargetElement("*", Attributes = nameof(Condition))]
    public class VisibilityTagHelper : TagHelper
    {
        public bool Condition { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
                output.SuppressOutput();

            base.Process(context, output);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
                output.SuppressOutput();

            return base.ProcessAsync(context, output);
        }
    }
}