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
//            OnePageClientViewModel OPC = new OnePageClientViewModel();
//            return View("Index", OPC);
			return View("Index");
		}

		public ActionResult OnePageClient()
		{
			OnePageClientViewModel OPC = OnePageClientViewModel.GetInstance();
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

			else if (Request.Form.AllKeys.Contains("RegenerateSessionId"))
			{
				ModelState.Clear();
				OPC.Transaction.SetUniqueSessionId(SessionIdGenerationMode.time, "");
			}

			else if (Request.Form.AllKeys.Contains("RegisterTransactionButton"))
			{
				// OPC.Transaction.P24 = OPC.P24;
				if (ModelState.IsValid)
				{
					try
					{
						OPC.Transaction.ThisTransactionNumber--;
						P24Response result = await OPC.Transaction.RegisterTransaction(true);
						try
						{
							if (result.OK)
							{
								ViewBag.TestResult = result.Token;
								if (Request.Form.AllKeys.Contains("P24.AutomaticRedirection") && Request.Form["P24.AutomaticRedirection"] == "True")
								{
									return Redirect(OPC.Transaction.GetRequestLink());
								}
							}
							else
							{
								ViewBag.TestResult = result.Error + " ";
								foreach (KeyValuePair<string, string> desc in result.Errors)
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
					}
				}
			}
			return View("OnePageClient", OPC);
		}

		public async Task<ActionResult> ManualTransactionConfirmation()
		{
			return View();
		}

		[HttpPost]
		public async Task<string> P24Status(VerifyTransactionHelper vth)
		{
			Przelewy24.Przelewy24 p24 = OnePageClientViewModel.GetInstance().P24;
			var result = await p24.VerifyTransaction(
								vth.p24_merchant_id,
								vth.p24_pos_id,
								vth.p24_session_id,
								vth.p24_amount,
								vth.p24_currency,
								vth.p24_order_id,
								vth.p24_method,
								vth.p24_statent,
								vth.p24_sign);
			return result.ToString();
		}

	}
}