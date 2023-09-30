using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime Dbn { get; set; }
        public Usuario() : base() 
        {

        }
    }
}
