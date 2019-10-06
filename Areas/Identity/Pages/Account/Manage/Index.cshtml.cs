using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHero.Data;
using MyHero.Controllers;

namespace MyHero.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _dbContext;

        public ApplicationUser _applicationUser;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Location")]
            public string Location { get; set; }

            [Display(Name = "Longitude")]
            public double Longitude { get; set; }

            [Display(Name = "Latitude")]
            public double Latitude { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

            [Display(Name = "Travel Distance")]
            public string Radius { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["USER"] = user;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.UserType is null)
            {
                return RedirectToPage("./ProfileSetup");
            }

            await LoadAsync(user);

            UserController con = new UserController(_dbContext);

            con.PopulateHeroRequestor(user, user.UserType.Contains("Hero"), user.UserType.Contains("Requestor"));

            this.GetUserSettings(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            this.SetUserSettings(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public void DefaultHeroRequestor()
        {
        }

        public void SetUserSettings(ApplicationUser _user)
        {
            UserController con = new UserController(_dbContext);
            Hero her = con.GetHero(_user.Id);

            her.Radius = Int32.Parse(this.Input.Radius);
            her.Location = this.Input.Location;
            her.Description = this.Input.Description;
            her.Latitude = this.Input.Latitude;
            her.Longitude = this.Input.Longitude;
            _dbContext.SaveChanges();
        }

        public void GetUserSettings(ApplicationUser _user)
        {
            UserController con = new UserController(_dbContext);
            Hero her = con.GetHero(_user.Id);

            this.Input.Radius = her.Radius.ToString();
            this.Input.Location = her.Location;
            this.Input.Latitude = her.Latitude;
            this.Input.Latitude = her.Longitude;
            this.Input.Description = her.Description;
        }
    }
}
