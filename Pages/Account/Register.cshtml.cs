using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityDemo.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public string? Username { get; set; }
        public string? Password { get; set; }

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Skapa en ny user med användarnamn och lösenord

            IdentityUser newUser = new()
            {
                UserName = Username
            };

            var createUserResult = await _userManager.CreateAsync(newUser, Password);

            if (createUserResult.Succeeded)
            {
                // Lyckats skapa en user...
                // Logga in!

                IdentityUser? userToLogIn = await _userManager.FindByNameAsync(Username);

                var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, Password, false, false);

                if (signInResult.Succeeded)
                {
                    // Omdirigera användaren till Members-sidan!
                    return RedirectToPage("/Member/Index");
                }
                else
                {
                    // Fel lösenord!
                }
            }
            else
            {

            }

            return Page();
        }
    }
}
