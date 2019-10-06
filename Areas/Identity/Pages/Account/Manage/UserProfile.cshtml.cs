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
    public partial class UserProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationDbContext _dbContext;

        public ApplicationUser _applicationUser;

        public UserProfileModel(
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

        [BindProperty]
        public InputModel2 Input2 { get; set; }

        public class InputModel2
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }
        }
        public class InputModel
        {
            [Display(Name = "Location")]
            public string Location { get; set; }

            [Display(Name = "Longitude")]
            public string Longitude { get; set; }

            [Display(Name = "Latitude")]
            public string Latitude { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;

            Input = new InputModel();

            Input2 = new InputModel2();
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
            Requestor her = con.GetRequestor(_user.Id);
            ApplicationUser usr = con.GetUser(_user.Id);

            her.Location = this.Input.Location;
            her.Description = this.Input.Description;
            her.Latitude = Double.Parse(this.Input.Latitude);
            her.Longitude = Double.Parse(this.Input.Longitude);
            usr.FirstName = this.Input2.FirstName;
            usr.LastName = this.Input2.LastName;
            _dbContext.SaveChanges();
        }

        public void GetUserSettings(ApplicationUser _user)
        {
            UserController con = new UserController(_dbContext);
            Requestor her = con.GetRequestor(_user.Id);
            ApplicationUser usr = con.GetUser(_user.Id);

            this.Input.Location = her.Location;
            this.Input.Latitude = her.Latitude + "";
            this.Input.Longitude = her.Longitude + "";
            this.Input.Description = her.Description;
            this.Input2.FirstName = usr.FirstName;
            this.Input2.LastName = usr.LastName;
        }
    }
}
