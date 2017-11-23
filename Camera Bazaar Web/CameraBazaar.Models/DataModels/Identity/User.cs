namespace CameraBazaar.Models.DataModels.Identity
{
    using System.Collections.Generic;
    using Camera;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public IEnumerable<Camera> Cameras { get; set; } = new List<Camera>();
    }
}
