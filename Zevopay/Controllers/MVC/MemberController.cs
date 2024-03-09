using Microsoft.AspNetCore.Mvc;
using Zevopay.Models;

namespace Zevopay.Controllers.MVC
{
    public class MemberController : Controller
    {
        public MemberController()
        {
        }

        public IActionResult UPIPayouts()
        {
            return View();
        }
        public IActionResult UPIPayoutsSaveAsync(UPIPayoutModel model)
        {
            return new JsonResult(new ResponseModel() { ResultFlag=1,Message="Payments successfully!"});
        }

        public IActionResult MoneyTransfer()
        {
            return View();
        }

        public IActionResult MoneyTransferSaveAsync(MoneyTransferModel model)
        {
            return new JsonResult(new ResponseModel() { ResultFlag = 1, Message = "Money successfully! Transferred" });
        }

        public IActionResult PayoutsLink()
        {
            return View();
        }


    }
}
