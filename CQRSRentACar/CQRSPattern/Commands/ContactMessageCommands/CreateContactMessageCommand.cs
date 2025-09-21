using System.ComponentModel.DataAnnotations;

namespace CQRSRentACar.CQRSPattern.Commands.ContactMessageCommands
{
    public class CreateContactMessageCommand
    {
        [Required(ErrorMessage = "Ad soyad gereklidir")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Konu gereklidir")]
        [StringLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir")]
        public string Subject { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Mesaj gereklidir")]
        [StringLength(1000, ErrorMessage = "Mesaj en fazla 1000 karakter olabilir")]
        public string Message { get; set; } = string.Empty;
    }
}
