using Zevopay.Models;

namespace Zevopay.Contracts
{
    public interface IApiService
    {
        Task<PayoutsMoneyTransferResponseModel> PayoutsMoneyTransferResponseAsync(PayoutsMoneyTransferRequestModel requestModel);
    }
}
