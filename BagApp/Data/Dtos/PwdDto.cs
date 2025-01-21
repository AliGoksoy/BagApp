using System.ComponentModel.DataAnnotations;

namespace BagApp.Data.Dtos
{
    public class PwdDto : IDto
    {
        [Required]
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
