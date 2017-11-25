using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Models.BindingModels.Admin
{
    public class UserAddToRoleBindingModel
    {
        [Required]
        public string Role{ get; set; }

        [Required]
        public string UserId { get; set; }  
    }
}
