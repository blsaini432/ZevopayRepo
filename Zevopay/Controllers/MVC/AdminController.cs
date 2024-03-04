using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Controllers.MVC
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            _userManager = userManager;
            _adminService = adminService;
        }





        #region Credit Debit Transaction
        public IActionResult AdminCreditDebitTransactions()
        {
            return View();
        }

        public async Task<IActionResult> AdminCreditDebitTransactionsPartial()
        {
            return PartialView(await _adminService.GetCeditDebitTransactions());

        }
        #endregion Credit Debit Transaction





        #region FundManage
        public async Task<IActionResult> FundForm(FundManageModel model)
        {
            try
            {
                model.Users = await _userManager.Users.Where(u => u.Role != RolesConstants.AdminRole).Select(x => new SelectListItem()
                {
                    Text = $"{x.MemberId}  {x.FirstName} {x.LastName}",
                    Value = $"{x.MemberId},{x.Id}",
                }).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FundManage(FundManageModel model)
        {
            ResponseModel response = new();
            try
            {
                //return Ok();
                response = await _adminService.FundManageAsync(model);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultFlag = 0;
            }
            return new JsonResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> GetBalanceByUser(string zevoId)
        {
            WalletTransactions response = new();
            try
            {
                response = await _adminService.GetBalanceByUser(zevoId);
            }
            catch (Exception ex)
            {
            }
            return new JsonResult(response);
        }

        #endregion FundManage End

        #region WalletTransaction
        public IActionResult WalletTransactions()
        {
            return View();
        }

        public async Task<IActionResult> WalletTransactionsPartial()
        {
            return PartialView(await _adminService.GetWalletTransactionsAsync());

        }
        #endregion WalletTransaction End

        #region Surcharge
        public IActionResult SurchargeList()
        {
            return View();
        }
        public async Task<IActionResult> SurchargePartial()
        {
            return PartialView(await _adminService.GetSurchagesAsync());
        }

        public IActionResult Surcharge()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Surcharge(Surcharge model)
        {
            ResponseModel response = new();

            try
            {
                response = await _adminService.AddSurchargeAsync(model);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultFlag = 0;
            }
            return new JsonResult(response);
        }
        #endregion Surcharge End
    }
}
