namespace CarDealer.Services.Models.Cars
{
    using System.Collections.Generic;
    using Parts;

    public class DetailedCarModel : CarModel
    {
        public IEnumerable<PartModel> Parts { get; set; }
    }
}
