using Microsoft.AspNetCore.Razor.TagHelpers;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Linq;

namespace OmerOzkan.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("getDutyByAppUserId")]
    public class DutyAppUserIdTagHelper : TagHelper
    {
        private readonly IDutyService _dutyService;
        public DutyAppUserIdTagHelper(IDutyService dutyService)
        {
            _dutyService = dutyService;
        }
        public string AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Duty> duties = _dutyService.GetByAppUserId(AppUserId);
            int completedDutyCount = duties.Where(I => I.Status).Count();
            int workingOnDutyCount = duties.Where(I => !I.Status).Count();

            string htmlString = $"<strong> Tamamladığı görev sayısı : </strong>{completedDutyCount} <br> <strong> Üstünde çalıştığı görev sayısı : </strong> {workingOnDutyCount}";

            output.Content.SetHtmlContent(htmlString);
        }
    }
}
