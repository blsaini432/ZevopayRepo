using Zevopay.Data.Entity;
using Zevopay.Models;

namespace Zevopay.Contracts
{
    public interface IAdminService
    {
        Task<ResponseModel> FundManageAsync(FundManageModel model);
        Task<IEnumerable<WalletTransactions>> GetWalletTransactionsAsync();
        Task<IEnumerable<Surcharge>> GetSurchagesAsync();
        Task<ResponseModel> AddSurchargeAsync(Surcharge model);

        Task<IEnumerable<WalletTransactions>> GetCeditDebitTransactions();

        Task<WalletTransactions> GetBalanceByUser(string  zevoId);
    }
}
