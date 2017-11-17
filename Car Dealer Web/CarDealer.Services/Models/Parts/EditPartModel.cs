namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class EditPartModel : PartModel
    {
        [Range(minimum:0,maximum:int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }    
    }
}
