﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DevConfSkopje.Web.Models;
using System.Linq;
using DevConfSkopje.Web.Models.Conference;
using System.IO;
using EmailServiceConf = DevConfSkopje.Services.EmailService;
using DevConfSkopje.Services.Contracts;
using DevConfSkopje.Services;
using System.Collections.Generic;
using System;

namespace DevConfSkopje.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ConferenceRegistrations");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("ConferenceRegistrations");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
       
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
      
        //[HttpPost, ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
        //            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("ConferenceRegistrations", "Account");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "DevConf");
        }

        [HttpGet]
        public ActionResult ConferenceRegistrations()
        {
            ConferenceRegistrationsViewModel model = new ConferenceRegistrationsViewModel();
            var registrations = _registrationsRepo.AllRegistrations().Where(x => x.IsValid == true).ToList();

            registrations.ForEach(x => model.ConferenceRegistrations.Add(new ConferenceRegistrationViewModel
            {
                Email = x.Email,
                FirstName = x.FirsName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }));

            return View(model);
        }

        [HttpGet]
        public ActionResult ExportRegistrations()
        {
            var registrations = _registrationsRepo.AllRegistrations().Where(x => x.IsValid == true).ToList();

            IExportRegistrationsService exportRegistrationsService 
                = new ExportRegistrationsService();

            var file = exportRegistrationsService.ExportToExcel(registrations);

            return File(file, "multipart/form-data", "DevConfSkopje2019-Registrations.xlsx");
        }

        [HttpGet]
        public ActionResult SendFeedbackEmails()
        {
            List<string> emails = _registrationsRepo.AllRegistrations().Where(x => x.Subscribe == true).Select(registration => registration.Email).ToList();
           // List<string> testEmails = new List<string>() { "istanchev@melontech.com","x3m.fall3n@gmail.com"};
            _emailService = new EmailServiceConf();

            var pathToTemplate = Server.MapPath(Url.Content("~/Content/EmailTemplate/thankyou.html"));

            try
            {
                _emailService.SendFeedbackMessages(emails, pathToTemplate);
            }
            catch (Exception ex)
            {
                return RedirectToRoute("GlobalError");
            }

            TempData["Success"] = "Feedback emails are sent successfully !";

            return RedirectToAction("ConferenceRegistrations");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}