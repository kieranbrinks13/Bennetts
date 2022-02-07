using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bennetts.TagHelpers
{
    [HtmlTargetElement("bennettsinput")]
    public class InputTagHelper : TagHelperBase
    {
        [HtmlAttributeName("asp-type")]
        public string Type { get; set; } = "text";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output != null)
            {
                base.Process(context, output);

                output.Content.SetHtmlContent(
                    @$"<div class='mb-3'>
                        <label for='{Id}' class='form-label'>{DisplayName}</label>
                        <input type='{Type}' class='form-control' id='{Id}' name='{Name}' value='{Source.Model}' {(Required ? "required" : "")} {(MaxLength.HasValue ? $"maxlength='{MaxLength}'" : "")} />
                        {Description}
                        {Validation}
                    </div>
                ");
            }
        }
    }
}
