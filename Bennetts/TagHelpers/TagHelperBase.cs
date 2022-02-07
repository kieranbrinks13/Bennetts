using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Bennetts.TagHelpers
{
    public class TagHelperBase : TagHelper
    {
        protected string Name => Source.Name;

        [HtmlAttributeName("asp-for")]
        public ModelExpression Source { get; set; }

        protected string Id => Source.Name.Replace(".", "_", System.StringComparison.InvariantCultureIgnoreCase);

        protected string DisplayName { get; set; }
        protected string? Description { get; set; }
        protected bool Required { get; set; }
        protected string? ErrorMessage { get; set; }

        protected string? Validation { get; set; }
        protected int? MaxLength { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output != null)
            {
                DisplayName = Source.Metadata.GetDisplayName();
                Description = $"<div class='form-text'>{Source.Metadata.Description}</div>";
                Required = Source.Metadata.IsRequired;
                var requiredAttribute = Source.Metadata.ValidatorMetadata.OfType<RequiredAttribute>().FirstOrDefault();
                if (requiredAttribute != null)              
                    ErrorMessage = requiredAttribute.FormatErrorMessage(DisplayName);

                if (Required && !string.IsNullOrWhiteSpace(ErrorMessage))
                    Validation = $"<div class='invalid-feedback'>{ErrorMessage}</div>";

                var maxLength = Source.Metadata.ValidatorMetadata.OfType<MaxLengthAttribute>().FirstOrDefault();
                MaxLength = maxLength?.Length ?? null;
            }
        }
    }
}
