using System.ComponentModel.DataAnnotations;

namespace UsuarioApi.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
