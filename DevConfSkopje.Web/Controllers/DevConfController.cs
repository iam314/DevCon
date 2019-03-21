﻿using DevConfSkopje.Web.Models.Conference;
using DevConfSkopje.DataModels;
using System.Web.Mvc;
using System.Linq;
using CaptchaMvc.HtmlHelpers;
using DevConfSkopje.Services.Contracts;
using System.IO;
using DevConfSkopje.Services;

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
            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            if(!this.IsCaptchaValid(""))
            {
                ViewBag.ErrorCaptcha = "Invalid captcha !";

                return View("Registration", model);
            }

            _registrationsRepo.AddNewRegistration(MapConfViewModelToDomainObj(model));
            _registrationsRepo.SaveDBChanges();

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
    }
}