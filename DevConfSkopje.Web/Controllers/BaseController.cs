using System.Web;
using System.Web.Mvc;
using DevConfSkopje.Data;
using DevConfSkopje.Data.Repository;
using Microsoft.AspNet.Identity.Owin;

namespace DevConfSkopje.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;
        protected RegistrationsRepository _registrationsRepo 
            = new RegistrationsRepository(new DevConfDbContext());

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult SuccessRegistration()
        {
            return View("_SuccessfulRegistration");
        }

        public ActionResult PageNotFound()
        {
            return View("NotFoundPage");
        }

        public ActionResult GlobalError()
        {
            return View("Error");
        }
    }
}