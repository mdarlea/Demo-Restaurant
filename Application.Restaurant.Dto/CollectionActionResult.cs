using System;
using System.Collections.Generic;
using Swaksoft.Core.Dto;

namespace Application.Restaurant.Dto
{
    public class CollectionActionResult<T> : ActionResult
    {
        public CollectionActionResult()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }
    }
}
