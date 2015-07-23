using System.Collections.Generic;

namespace Application.Restaurant.Dto
{
    public class ActionResult : Swaksoft.Core.Dto.ActionResult
    {
        public IEnumerable<string> Errors { get; set; } 
    }
}
