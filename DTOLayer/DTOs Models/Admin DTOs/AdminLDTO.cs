using System.ComponentModel.DataAnnotations;


namespace DTOLayer.DTOs_Models.Admin_DTOs
{
    public class AdminLDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Email Address Is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string? Password { get; set; }
    }
}
