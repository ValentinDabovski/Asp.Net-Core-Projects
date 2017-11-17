namespace CarDealer.Services.Models
{
    using System.Collections.Generic;

    public class DetailedCarModel : CarModel
    {
        public IEnumerable<PartsModel> Parts { get; set; }
    }
}
