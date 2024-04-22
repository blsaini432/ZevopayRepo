using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Contracts
{
    public interface IMemberService
    {
        Task<WalletModel> GetWalletBalanceRecordAsync(string userId);
        Task<IEnumerable<MemberWalletTransactions>> GetWalletTransactionsAsync(string userId);
        Task<IEnumerable<PayoutsMoneyTransferRequestModel>> GetPayoutTransactionsAsync(string memberId);
        Task<Surcharge> GetSurchargeBasedPackageAsync();
        Task<ResponseModel> CheckWalletBalanceAndUpdateAsync(decimal amount,string userId);
    }
}
