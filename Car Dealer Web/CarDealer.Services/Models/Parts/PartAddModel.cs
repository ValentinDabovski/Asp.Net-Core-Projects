namespace CarDealer.Services.Models.Parts
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class PartAddModel : PartModel
    {
        [Range(minimum:1, maximum:int.MaxValue)]
        public int  Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }  
    }
}
