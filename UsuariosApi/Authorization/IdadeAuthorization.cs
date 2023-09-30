using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var DbnClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

            if (DbnClaim is null)
            {
                return Task.CompletedTask;
            }

            var Dbn = Convert.ToDateTime(DbnClaim.Value);

            var idadeUsuario = DateTime.Today.Year - Dbn.Year;

            if (Dbn >
                DateTime.Today.AddYears(-idadeUsuario))
                idadeUsuario--;

            if (idadeUsuario >= requirement.Idade)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
