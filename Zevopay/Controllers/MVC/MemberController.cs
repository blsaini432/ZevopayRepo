using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Controllers.MVC
{
    public class MemberController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;
        private readonly IMemberService _memberService;

        public MemberController(UserManager<ApplicationUser> userManager, IAdminService adminService, IMemberService memberService)
        {
            _userManager = userManager;
            _adminService = adminService;
            _memberService = memberService;
        }

        #region UPIPayouts
        public IActionResult UPIPayouts()
        {
            return View();
        }
        public async Task<IActionResult> UPIPayoutsSaveAsync(UPIPayoutModel model)
        {
            string userId = GetCurrentUserAsync().Result.Id;

            var updateWalletResult = await UpdateWalletAsync(userId, model.Amount);

            if (updateWalletResult != null && updateWalletResult.ResultFlag == 0) return new JsonResult(updateWalletResult);


            return new JsonResult(new ResponseModel() { ResultFlag = 1, Message = "Payments successfully!" });
        }

        #endregion UPIPayouts End

        #region MoneyTransfer

        public IActionResult MoneyTransfer()
        {
            return View();
        }

        public async Task<IActionResult> MoneyTransferSaveAsync(MoneyTransferModel model)
        {
            string userId = GetCurrentUserAsync().Result.Id;

            var updateWalletResult = await UpdateWalletAsync(userId, model.Amount);

            if (updateWalletResult != null && updateWalletResult.ResultFlag == 0) return new JsonResult(updateWalletResult);

            return new JsonResult(new ResponseModel() { ResultFlag = 1, Message = "Money successfully! Transferred" });
        }
        #endregion MoneyTransfer End

        #region PayoutsLink
        public IActionResult PayoutsLink()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PayoutsLinkSaveAsync()
        {
            /*string userId = GetCurrentUserAsync().Result.Id;

            var updateWalletResult = await UpdateWalletAsync(userId, model.Amount);

            if (updateWalletResult != null && updateWalletResult.ResultFlag == 0) return new JsonResult(updateWalletResult);*/

            return new JsonResult(new ResponseModel() { ResultFlag = 1, Message = "Link successfully! Sent" });
        }
        #endregion PayoutsLink End


        public async Task<ResponseModel> UpdateWalletAsync(string userId, decimal amount)
        {
            var walletBalance = _adminService.GetBalanceByUser(userId).Result;

            if (walletBalance.Balance < amount) return new ResponseModel() { Message = "Insuficiance Balance!", ResultFlag = 0 };

            var updateFundModel = new FundManageModel()
            {
                Amount = amount,
                Factor = "Dr",
                MemberId = walletBalance.MemberId,
                Description = "Amount Debited for upi payouts"
            };
            return await _adminService.FundManageAsync(updateFundModel);
        }

        public async Task<IActionResult> Wallet()
        {
            try
            {
                string userId = GetCurrentUserAsync().Result.Id;
                var result = await _memberService.GetWalletBalanceRecordAsync(userId);
                return View(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        public async Task<IActionResult> WalletTransactions()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> WalletTransactionsPartial()
        {
            try
            {
                return PartialView( await _memberService.GetWalletTransactionsAsync(GetCurrentUserAsync().Result.Id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region GetUser
        public async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
        #endregion GetUser End
    }
}
