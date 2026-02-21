using Microsoft.AspNetCore.Identity;
using Clinica.Api.Common.Api;
using Clinica.Api.Models;

namespace Clinica.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/logout", HandleAsync)
            .WithSummary("Deslogar")
            .WithDescription("Deslogar")
            .RequireAuthorization();
    }

    private static async Task<IResult> HandleAsync(SignInManager<Usuario> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
}
