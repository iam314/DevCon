using DevConfSkopje.Web.Models.Conference;
using DevConfSkopje.DataModels;
using System.Web.Mvc;
using System.Linq;
using EmailServiceConf = DevConfSkopje.Services.EmailService;
using System;

namespace DevConfSkopje.Web.Controllers
{
    public class DevConfController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome";

            return View();
        }

        [HttpPost]
        public ActionResult ConferenceRegistration(ConferenceRegistrationViewModel model)
        {
            CheckForDublicatedEmails(model.Email);

            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            //string googleRecaptcha = Request.Form["g-recaptcha-response"];
            //var validateRecaptcha = ReCaptchaHelper.VerifyGoogleReCaptcha(googleRecaptcha);
            //if (validateRecaptcha == null || !validateRecaptcha.Success)
            //{
            //    return View("Registration", model);
            //}

            _emailService = new EmailServiceConf();
            _registrationsRepo.AddNewRegistration(MapConfViewModelToDomainObj(model));
            _registrationsRepo.SaveDBChanges();

            try
            {
                var pathToTemplate = Server.MapPath(Url.Content("~/Content/EmailTemplate/index.html"));
                var pathToImage = Server.MapPath(Url.Content("~/Content/images/hero.jpg"));
                var pathToLogo = Server.MapPath(Url.Content("~/Content/images/logo.png"));
                _emailService.SendCorfimation(model.Email, pathToTemplate, pathToImage, pathToLogo);
            }
            catch (Exception ex)
            {
                return RedirectToRoute("GlobalError");
            }

            return RedirectToRoute("RegSuccess");
        }

        #region STATIC PAGES
        public ActionResult Faq()
        {
            ViewBag.Title = "Faq";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        public ActionResult Speakers()
        {
            ViewBag.Title = "Speakers";

            return View();
        }

        public ActionResult Schedule()
        {
            ViewBag.Title = "Schedule";

            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Title = "Registration";

            int registrationsLimit = _registrationsRepo.AllRegistrations().Where(x => x.IsValid == true).Count();

            if (registrationsLimit > 350)
            {
                return View("RegistrationsLimit");
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            return View();
        }

        public ActionResult TermsAndConditions()
        {
            ViewBag.Title = "TermsAndConditions";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Title = "Privacy";

            return View();
        }

        public ActionResult CookiePolicy()
        {
            ViewBag.Title = "CookiePolicy";

            return View();
        }

        public ActionResult CodeOfConduct()
        {
            ViewBag.Title = "Code of conduct";

            return View();
        }
        #endregion

        private ConferenceRegistration MapConfViewModelToDomainObj(ConferenceRegistrationViewModel model)
        {
            ConferenceRegistration domainObj = new ConferenceRegistration();

            domainObj.FirsName = model.FirstName;
            domainObj.LastName = model.LastName;
            domainObj.Email = model.Email;
            domainObj.PhoneNumber = model.PhoneNumber;
            domainObj.IsValid = true;

            return domainObj;
        }

        private void CheckForDublicatedEmails(string email)
        {
            bool isValid = _registrationsRepo.CheckRegistrationEmail(email);

            if (isValid)
            {
                ModelState.AddModelError("Email", "You have already registered for the conference!");
            }
        }
    }
}