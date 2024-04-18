﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;
using static Azure.Core.HttpHeader;
using static QRCoder.PayloadGenerator.SwissQrCode;

namespace Zevopay.Controllers.MVC
{
    public class MemberController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;
        private readonly IMemberService _memberService;
        private readonly ICommonService _commonService;
        private readonly IApiService _apiService;
        private readonly IPayoutsService _payoutsService;

        public MemberController(UserManager<ApplicationUser> userManager, IAdminService adminService, IMemberService memberService, ICommonService commonService, IApiService apiService, IPayoutsService payoutsService)
        {
            _userManager = userManager;
            _adminService = adminService;
            _memberService = memberService;
            _commonService = commonService;
            _apiService = apiService;
            _payoutsService = payoutsService;
        }

        #region UPIPayouts
        public IActionResult UPIPayouts()
        {
            return View();
        }
        public async Task<IActionResult> UPIPayoutsSaveAsync(UPIPayoutModel model)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User) ?? new();

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
            ResponseModel response = new();
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User) ?? new();
                response = await _payoutsService.PayoutsUsingBankAccountAsync(user, model);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultFlag = 0;
            }
            return new JsonResult(response);
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



        public async Task<IActionResult> Wallet()
        {
            try
            {
                string userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;

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
                return PartialView(await _memberService.GetWalletTransactionsAsync(_userManager.GetUserAsync(HttpContext.User).Result.Id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
