using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DDDSouthWest.Website.Components
{
    public class ValidationSummaryTagHelper : TagHelper
    {
        public IList<ValidationFailure> Errors { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Errors == null || !Errors.Any())
                return;

            output.TagMode = TagMode.StartTagAndEndTag;

            var panel = @"
            <ul class=""error-list"">";
            foreach (var error in Errors)
            {
                panel += $"<li>{error.ErrorMessage}</li>";
            }
            panel += "</ul>";
            output.Content.SetHtmlContent(panel);
        }
    }
}