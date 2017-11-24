namespace LearningSystem.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserAddToRoleBindingModel
    {
        [Required]
        public string Role{ get; set; }

        [Required]
        public string UserId { get; set; }  
    }
}
