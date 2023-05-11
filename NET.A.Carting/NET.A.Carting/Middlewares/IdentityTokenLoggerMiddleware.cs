using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NET.A.Carting.Middlewares
{
    public class IdentityTokenLoggerMiddleware : IMiddleware
    {
        private readonly ILogger<IdentityTokenLoggerMiddleware> _logger;

        public IdentityTokenLoggerMiddleware(ILogger<IdentityTokenLoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Extract the identity access token from the Authorization header
            var accessToken = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(accessToken))
            {
                var tok = accessToken.Replace("Bearer ", "");
                var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);

                var jti = jwttoken.Claims.FirstOrDefault(cl => cl.Type == "email")?.Value;
                context.Items.Add("Email", jti);
            }

            // Get the ClaimsPrincipal for the authenticated user
            var user = context.User;

            // Get the value of the name claim
            var name = user.FindFirstValue("Name");

            // Log the identity access token
            _logger.LogInformation($"Identity access token for {name}: {accessToken}");

            // Call the next middleware in the pipeline
            await next(context);
        }
    }

}
