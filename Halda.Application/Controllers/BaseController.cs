using Halda.Application.Models;
using Halda.Core.DTO;
using Halda.Core.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Halda.Application.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly HttpClient _client;
        protected CompanyRole ComRole;
        public BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Halda");
            //var ComId = new string(Request.Cookies["ComId"].ToString());
            //var UserId = new string(Request.Cookies["UserId"].ToString());

            //_client.DefaultRequestHeaders.Add("UserId", UserId.ToString());
            //_client.DefaultRequestHeaders.Add("ComId", ComId.ToString());
        }

        protected string GetAccessTokenFromStorage()
        {
            return Request.Cookies["access_token"];
        }
        protected string GetRefreshTokenFromStorage()
        {
            return Request.Cookies["refresh_token"];
        }
        protected bool IsAccessTokenExpired(string accessToken)
        {
            // Parse the access token to extract the expiration claim
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(accessToken);

            // Get the expiration time from the token
            DateTime expirationTime = token.ValidTo;

            // Check if the current time is after the token's expiration time
            bool isExpired = DateTime.UtcNow > expirationTime;

            return isExpired;
        }

        protected async Task<bool> TryRefreshAccessTokenAsync()
        {
            string accessToken = GetAccessTokenFromStorage();
            if (string.IsNullOrEmpty(accessToken))
            {
                await HttpContext.SignOutAsync("Chitra");
                return false;

            }// Retrieve the access token from storage
            bool isAccessTokenExpired = IsAccessTokenExpired(accessToken); // Check if the access token is expired

            // Get Role
            //ComRole = GetUserRole();

            if (isAccessTokenExpired)
            {
                string refreshToken = GetRefreshTokenFromStorage(); // Retrieve the refresh token from storage
                if (string.IsNullOrEmpty(refreshToken))
                {
                    await HttpContext.SignOutAsync("Chitra");
                    return false;

                }
                var client = _httpClientFactory.CreateClient("Chitra");
                // Send a request to the token refresh endpoint
                //HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Chitra-API-Key", "ChitraManechitramanebagbonerchitranakintoatagtrarchitra");
                var result = await client.PostAsync($"Auth/RefreshToken?refreshTokenRequest={refreshToken}&comId={User.Claims.FirstOrDefault(x => x.Type == "CompanyId").Value}", null);
                TokenResult resultToken = new TokenResult();
                if (result.IsSuccessStatusCode)
                {
                    resultToken = await result.Content.ReadFromJsonAsync<TokenResult>();
                    await DecodeTokenAndCreateCookie(resultToken.accessToken, HttpContext);
                    Response.Cookies.Append("access_token", resultToken.accessToken, new CookieOptions
                    {
                        HttpOnly = true
                    });
                    return true;
                }
                else
                {
                    //var error = await result.Content.ReadAsStringAsync();
                    await HttpContext.SignOutAsync("Chitra");
                    return false;

                }


            }
            return true;

        }
        private async Task DecodeTokenAndCreateCookie(string encodedToken, HttpContext httpContext)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(encodedToken);


            var identity = new ClaimsIdentity(token.Claims, "User");

            // Create an authentication ticket with the claims identity
            var principal = new ClaimsPrincipal(identity);
            var authenticationProperties = new AuthenticationProperties
            {

                ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.Claims.FirstOrDefault(x => x.Type == "exp")?.Value))
            };

            await httpContext.SignInAsync("Chitra", principal, authenticationProperties);
        }


        private CompanyRole GetUserRole()
        {
            // Get the current user's claims from the HttpContext
            var claims = HttpContext.User?.Claims;

            // Find the role claim (assuming the claim type is "role" in the JWT token)
            var roleClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Convert the role claim to the UserRole enum (assuming UserRole is an enum)
            if (Enum.TryParse(roleClaim, out CompanyRole userRole))
            {
                return userRole;
            }

            // If the role claim is not present or invalid, return a default role (e.g., Guest)
            return userRole;
        }











        // Override the OnActionExecuting method to perform token refresh before executing each action
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Task<bool> tokenRefreshTask = TryRefreshAccessTokenAsync();
            tokenRefreshTask.Wait(); // Wait for the token refresh task to complete synchronously

            if (tokenRefreshTask.Result)
            {
                // Token refresh was successful, continue executing the action
                return;
            }
            filterContext.Result = RedirectToAction("Login", "Account", new { area = "" });


        }
    }
}
