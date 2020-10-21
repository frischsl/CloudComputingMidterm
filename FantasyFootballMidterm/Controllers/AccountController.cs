using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FantasyFootballMidterm.Models;

namespace FantasyFootball.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set 
        //    { 
        //        _signInManager = value; 
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            usp_Users_Login_Result user;
            using (var context = new FantasyFootballEntities())
            {
                user = context.usp_Users_Login(model.Username, model.Password).FirstOrDefault();
            }

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("InvalidUser", "Incorrect Username or Password.");

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult Register(User model)
        {

            if (ModelState.IsValid)
            {
                if (model.Email != null && model.Username != null && model.Password != null)
                {
                    int? userID;
                    using (var context = new FantasyFootballEntities())
                    {
                        userID = context.usp_Users_SaveNewUser(model.Username, model.Password, model.Email).FirstOrDefault();
                    }

                    if (userID != null)
                    {
                        Session["UserID"] = userID;
                        Session["Username"] = model.Username;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("DuplicateUsers", "A user has already been created with this Email or Username.");
                }
                else
                    ModelState.AddModelError("DuplicateUsers", "Please fill out all of the fields.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // POST: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    if (_userManager != null)
            //    {
            //        _userManager.Dispose();
            //        _userManager = null;
            //    }

            //    if (_signInManager != null)
            //    {
            //        _signInManager.Dispose();
            //        _signInManager = null;
            //    }
            //}

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";


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

        }
        #endregion
    }
}