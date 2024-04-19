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

        #region PayoutsBankAccount
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
                Mode = model.PaymentMode ?? string.Empty,
                Fund_account = new FundAccount
                {
                    Account_type = "bank_account",
                    Bank_account = new BankAccount
                    {
                        Name = model.FullName ?? string.Empty,
                        Ifsc = model.IFSCCode,
                        Account_number= model.AccountNumber.ToString()
                    },
                    Contact = new Contact
                    {
                        Name = "Ramlakhan",
                        Email = "Ramlakhan@example.com",
                        contact = "9876543210",
                        Type = "vendor",
                        Reference_id = "Acme Contact ID 12345",
                        Notes = new Notes
                        {
                            Notes_key_1 = "Tea, Earl Grey, Hot",
                            Notes_key_2 = "Tea, Earl Grey… decaf."
                        }

                    }
                },
                Account_number = "2323230098018072",
                Amount = (int)model.Amount,
                Currency = "INR",
                Purpose = "refund",
                Queue_if_low_balance = true,
                Reference_id = referenceId,
                Narration = "Acme Corp Fund Transfer",
                Notes = new Notes
                {
                    Notes_key_1 = "Tea, Earl Grey, Hot",
                    Notes_key_2 = "Tea, Earl Grey… decaf."
                }
            };

            var apiResponse = await _apiService.PayoutsMoneyTransferResponseAsync(requestPayouts);

            if (apiResponse?.Error != null && !string.IsNullOrEmpty(apiResponse?.Error?.Description))
            {
                response.Message = apiResponse?.Error?.Description ?? string.Empty;
                response.ResultFlag = 0;
                return response;
            }

            return new ResponseModel() { ResultFlag = 1, Message = "Money successfully! Transferred" };
        }
        #endregion PayoutsBankAccount End

        #region UPIPayouts
        public async Task<ResponseModel> UpiPayoutAsync(ApplicationUser user, UPIPayoutModel model)
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

            var upiPayoutmodel = new UpiPayoutRequestModel
            {
                Account_number = PayoutAccountNumber,
                Amount = (int)model.Amount,
                Currency = "INR",
                Mode = "UPI",
                Purpose = "refund",
                Fund_account = new FundAccountUPI
                {
                    Account_type = "vpa",
                    Vpa = new Vpa
                    {
                        Address = model.UPIId
                    },
                    Contact = new Contact
                    {
                        Name = "Gaurav Kumar",
                        Email = "gaurav.kumar@example.com",
                        contact = "9876543210",
                        Type = "self",
                        Reference_id = "Acme Contact ID 12345",
                        Notes = new Notes
                        {
                            Notes_key_1 = "Tea, Earl Grey, Hot",
                            Notes_key_2 = "Tea, Earl Grey… decaf."
                        }
                    }
                },
                Queue_if_low_balance = true,
                Reference_id = referenceId,
                Narration = "Acme Corp Fund Transfer",
                Notes = new Notes
                {
                    Notes_key_1 = "Tea, Earl Grey, Hot",
                    Notes_key_2 = "Tea, Earl Grey… decaf."
                }
            };

            var apiResponse = await _apiService.UPIPayoutsAsync(upiPayoutmodel);

            if (apiResponse?.Error != null && !string.IsNullOrEmpty(apiResponse?.Error?.Description))
            {
                response.Message = apiResponse?.Error?.Description ?? string.Empty;
                response.ResultFlag = 0;
                return response;
            }

            return new ResponseModel() { ResultFlag = 1, Message = "Money successfully! Transferred" };
        }
        #endregion UPIPayouts End

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
