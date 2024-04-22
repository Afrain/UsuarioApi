using Microsoft.AspNetCore.Identity;

namespace UsuarioApi.Models;

public class Usuario : IdentityUser
{
    public DateTime DataNascimento { get; set; }

    public Usuario() : base()
    {
        
    }
}
