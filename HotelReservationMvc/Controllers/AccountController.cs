using HotelReservationMvc.Data;
using HotelReservationMvc.EmailSenderProcesess;
using HotelReservationMvc.Models;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HotelReservationMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailManager _emailManager;

        public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailManager emailManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailManager = emailManager;
        }

        // Displays the login page
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        // Handles user login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            // Find user by email
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                // Check if password is correct
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        // Redirect to home page on successful login
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginVM);
        }

        // Displays the registration page
        public IActionResult Register()
        {
            var result = new RegisterViewModel();
            return View(result);
        }

        // Handles account activation from the email link
        [HttpGet]
        public async Task<IActionResult> Activation(string u, string t)
        {
            if (u == null || t == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(u);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(t));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            // Redirect to login page if activation is successful
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // Handles user registration
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            // Check if email is already in use
            var user = await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVm);
            }

            // Create a new user
            var newUser = new AppUser()
            {
                Email = registerVm.EmailAddress,
                UserName = registerVm.EmailAddress,
                EmailConfirmed = false
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.WaitForValidation);
            }

            // Generate email confirmation token
            var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser).Result;
            var encToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // Build the activation URL
            var url = Url.Action("Activation", "Account", new { u = newUser.Id, t = encToken }, protocol: Request.Scheme);

            // Send the confirmation email
            _emailManager.SendEmail(new EmailMessageModel()
            {
                Subject = $"AddrebookEdu - WELCOME - ACTIVATION PROCESS",
                To = newUser.Email,
                Body = $"<b>Hello {newUser.Email},</b><br/>" +
                       $"<h4>Your registration is successful!</h4><br/>" +
                       $"To use the system, you need to activate your account. Click <a href='{url}' target='_blank'>here</a> to activate."
            });

            TempData["RegisterSuccessMsg"] = $"Dear {newUser.Email}, your registration is successful. An activation email will be sent within a few minutes!";

            return View("Login", "Account");
        }

        // Logs the user out
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // Displays the forget password page
        public IActionResult ForgetPassword()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        // Handles forget password process
        [HttpPost]
        public IActionResult ForgetPassword(string? emailorUsername)
        {
            try
            {
                if (string.IsNullOrEmpty(emailorUsername))
                {
                    ModelState.AddModelError("", "You didn't enter Email / Username!!!");
                    return View();
                }

                // Try to find the user
                var user = _userManager.FindByEmailAsync(emailorUsername).Result;
                if (user == null)
                {
                    user = _userManager.FindByNameAsync(emailorUsername).Result;
                }

                if (user == null)
                {
                    ModelState.AddModelError("", "Are you sure you are registered in the system....");
                    return View();
                }

                // Generate password reset token
                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                var encodeToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var useridToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id));

                // Build the password recovery URL
                var url = Url.Action("RecoverPassword", "Account", new { u = useridToken, t = encodeToken }, protocol: Request.Scheme);

                // Send password reset email
                bool emailResult = _emailManager.SendEmail(new EmailMessageModel()
                {
                    To = user.Email,
                    Subject = "AddressBookEdu - I FORGOT MY PASSWORD!",
                    Body = $"<b>Hello {user.Email},</b><br/>"
                              + $"We are sending this email because you forgot your password. Click <a href='{url}'>here</a> to reset your password."
                });

                if (emailResult)
                {
                    ViewBag.ForgetPasswordSuccessMsg = $"Dear {user.Email}, we have sent a password reset email to you.";
                }
                else
                {
                    ViewBag.ForgetPasswordFailedMsg = $"Email could not be sent! Please try again!";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ForgetPasswordFailedMsg = $"An unexpected error occurred! {ex.Message}";
                // Log the exception
                return View();
            }
        }

        // Displays the password recovery page
        [HttpGet]
        public IActionResult RecoverPassword(string? u, string? t)
        {
            try
            {
                if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(t))
                {
                    ModelState.AddModelError("", "Required parameters are missing! You cannot proceed!");
                    return View();
                }

                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(t));
                var userid = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(u));

                // Find the user by ID
                var user = _userManager.FindByIdAsync(userid).Result;
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found! You cannot proceed!");
                    return View();
                }

                // Create the model for password recovery
                RecoverPasswordVM model = new RecoverPasswordVM()
                {
                    Userid = userid,
                    User = user,
                    Token = token
                };
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An unexpected error occurred!");
                return View(new RecoverPasswordVM());
            }
        }

        // Handles password recovery
        [HttpPost]
        public IActionResult RecoverPassword(RecoverPasswordVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", $"Please enter valid inputs!");
                    return View(model);
                }

                // Find the user by ID
                var user = _userManager.FindByIdAsync(model.Userid).Result;

                // Reset the user's password
                var result = _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword).Result;

                if (!result.Succeeded)
                {
                    string errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                    ModelState.AddModelError("", $"An error occurred during the password reset process! {errorMessage}");
                    return View(model);
                }
                ViewBag.RecoverPasswordSuccessMsg = $"Dear {user.Email}, your password has been successfully reset. You can log in now.";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An unexpected error occurred! {ex.Message}");
                return View(model);
            }
        }
    }
}
