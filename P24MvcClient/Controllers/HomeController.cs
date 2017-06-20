using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Przelewy24;

namespace P24MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private Przelewy24.Przelewy24 p24;

        public HomeController()
        {
            this.p24 = new Przelewy24.Przelewy24(0, 0, "");
        }

        //
        // GET: /Home/
        
        public async Task<ActionResult> Index()
        {
            var keys =  Request.Form.AllKeys;
            string merchantId = Request.Form.Get ("MerchantId");
            ViewBag.MerchantId = merchantId;
            this.p24.MerchantId = merchantId;

            string posId = Request.Form.Get("PosId");
            ViewBag.PosId = posId;
            this.p24.PosId = posId;

            string crcKey = Request.Form.Get("Crckey");
            ViewBag.Crckey = crcKey;
            this.p24.CrcKey = crcKey;

            if (Request.Form.AllKeys.Contains("SandBoxMode"))
            {
                if (Request.Form.Get("SandBoxMode").Substring(0, 4) == "true")
                {
                    ViewBag.SandBoxMode = "checked";
                    this.p24.SandboxMode = true;
                }
            }
            else
                this.p24.SandboxMode = false;

            if(Request.Form.AllKeys.Count() > 0 )
            {
                var testResult = await this.p24.TestConnection();
                ViewBag.TestResult = testResult.ToString();
            }

            return View(p24);
        }
        
        public async Task<ActionResult> TestConnection()
        {
            var testResult = await this.p24.TestConnection ();
            ViewBag.TestResult = testResult.ToString ();
            return View ();
        }
	}
}