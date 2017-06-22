using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Przelewy24;
using P24MvcClient.Models;

namespace P24MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        { }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            OnePageClientViewModel OPC = new OnePageClientViewModel();
            return View("Index", OPC);
        }

        public ActionResult OnePageClient()
        {
            OnePageClientViewModel OPC = new OnePageClientViewModel();
            return View("OnePageClient", OPC);
        }

        [HttpPost]
        public async Task<ActionResult> OnePageClient(OnePageClientViewModel OPC)
        {
            if(Request.Form.AllKeys.Contains("TestButton"))
            {
                if (ModelState.IsValidField("P24.CrcKey"))
                {
                    var result = await OPC.P24.TestConnection();
                    ViewBag.TestResult = result.ToString();
                }
                
            }
            else if (Request.Form.AllKeys.Contains("RegisterTransactionButton"))
            {
                OPC.Transaction.P24 = OPC.P24;
                if (ModelState.IsValid)
                {
                    var result = await OPC.Transaction.RegisterTransaction();
                    try
                    {
                        try
                        {
                            Przelewy24.P24Response resp = new P24Response(result.ToString());
                            if (resp.OK)
                            {
                                ViewBag.TestResult = resp.Token;
                            }
                            else
                            {
                                ViewBag.TestResult = resp.Error + " ";
                                foreach (KeyValuePair<string, string> desc in resp.Errors)
                                {
                                    ViewBag.TestResult += desc.Key + " : " + desc.Value + " ";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.DebugData += ex.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.DebugData += ex.ToString();
                        ViewBag.TestResult = result.ToString();
                    }
                }
            }
            return View("OnePageClient", OPC);
        }
	}
}