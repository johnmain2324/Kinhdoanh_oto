using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace Kinhdoanh_oto.Pages.Taikhoan
{
    public class DangnhapModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSignInWithFirebaseAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    var json = JsonDocument.Parse(body);
                    var root = json.RootElement;

                    if (!root.TryGetProperty("idToken", out var tokenElement))
                    {
                        return BadRequest("Missing idToken");
                    }

                    var idToken = tokenElement.GetString();

                    if (string.IsNullOrWhiteSpace(idToken))
                    {
                        return BadRequest("Invalid idToken");
                    }

                    var decoded = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

                    var uid = decoded.Uid;
                    var email = decoded.Claims.ContainsKey("email") ? decoded.Claims["email"].ToString() : "";
                    var name = decoded.Claims.ContainsKey("name") ? decoded.Claims["name"].ToString() : email;

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, uid),
                new Claim(ClaimTypes.Email, email ?? ""),
                new Claim(ClaimTypes.Name, name ?? ""),
                new Claim("FirebaseUid", uid)
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = true }
                    );

                    return new OkResult();
                }
            }
            catch (FirebaseAuthException ex)
            {
                return BadRequest($"Invalid Firebase token: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}