using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zevopay.Contracts;
using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Services
{
    public class PayoutsService : IPayoutsService
    {
        #region GlobalVariables
        private readonly IAdminService _adminService;
        private readonly ICommonService _commonService;
        private readonly IApiService _apiService;
        private readonly string PayoutAccountNumber = "2323230098018072";
        #endregion GlobalVariables End

        #region Constructor
        public PayoutsService(IAdminService adminService, ICommonService commonService, IApiService apiService)
        {
            _adminService = adminService;
            _commonService = commonService;
            _apiService = apiService;
        }
        #endregion Constructor End

        #region PayoutsMoneyTransfer
        public async Task<ResponseModel> PayoutsUsingBankAccountAsync(ApplicationUser user, MoneyTransferModel model)
        {
            ResponseModel response = new();
            SurchargeModel surcharge = new();
            string referenceId = _commonService.RandamNumber(10);

            if (surcharge.IsFlat && surcharge.SurchargeAmount > 0)
                model.Amount = model.Amount + (model.Amount * surcharge.SurchargeAmount / 100);
            else if (!surcharge.IsFlat && surcharge.SurchargeAmount > 0)
                model.Amount = model.Amount + surcharge.SurchargeAmount;

            var updateWalletResult = await UpdateWalletAsync(user.Id, user.MemberId, model.Amount);

            if (updateWalletResult != null && updateWalletResult.ResultFlag == 0) return updateWalletResult;

            var requestPayouts = new PayoutsMoneyTransferRequestModel
            {
                mode = model.PaymentMode ?? string.Empty,
                fund_account = new FundAccountMoneyTransRequestModel()
                {
                    account_type = "bank_account",
                    bank_account = new BankAccountMoneyTransRequestModel()
                    {
                        name = model.FullName ?? string.Empty,
                        ifsc = model.IFSCCode,
                        account_number = model.AccountNumber.ToString()
                    },
                    contact = new ContactMoneyTransRequestModel
                    {
                        name = "Ramlakhan",
                        email = "Ramlakhan@example.com",
                        contact = "9876543210",
                        type = "vendor",
                        reference_id = "Acme Contact ID 12345",
                        notes = new NotesMoneyTransRequestModel()
                        {
                            notes_key_1 = "Tea, Earl Grey, Hot",
                            notes_key_2 = "Tea, Earl Grey… decaf."
                        }

                    }
                },
                account_number = PayoutAccountNumber,
                amount = (int)model.Amount,
                currency = "INR",
                purpose = "refund",
                queue_if_low_balance = true,
                reference_id = referenceId,
                narration = "Acme Corp Fund Transfer",
                notes = new NotesMoneyTransRequestModel()
                {
                    notes_key_1 = "Tea, Earl Grey, Hot",
                    notes_key_2 = "Tea, Earl Grey… decaf."
                }
            };

            var apiResponse = await _apiService.PayoutsMoneyTransferResponseAsync(requestPayouts);

            if (apiResponse?.error != null && !string.IsNullOrEmpty(apiResponse?.error?.description.ToString()))
            {
                response.Message = apiResponse?.error?.description?.ToString() ?? string.Empty;
                response.ResultFlag = 0;
                return response;
            }

            return new ResponseModel() { ResultFlag = 1, Message = "Money successfully! Transferred" };
        }
        #endregion PayoutsMoneyTransfer End

        #region UpdateWallet
        public async Task<ResponseModel> UpdateWalletAsync(string userId, string memberId, decimal amount)
        {
            var walletBalance = _adminService.GetBalanceByUser(userId).Result;

            if (walletBalance.Balance < amount) return new ResponseModel() { Message = "Insuficiance Balance!", ResultFlag = 0 };

            var updateFundModel = new FundManageModel()
            {
                Amount = amount,
                Factor = "Dr",
                MemberId = memberId,
                Description = "Amount Debited for upi payouts"
            };
            return await _adminService.FundManageAsync(updateFundModel);
        }
        #endregion UpdateWallet End
    }
}
