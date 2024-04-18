using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;
using Zevopay.Contracts;
using Zevopay.Models;

namespace Zevopay.Services
{
    public class ApiService : IApiService
    {

        private readonly string apiKey = "rzp_test_lpwYKAvQbwM8P7";
        private readonly string secretKey = "RroYahFulRKmMxsAPTo69XMV";
        private readonly string apiUrl = "https://api.razorpay.com/v1/";
        public async Task<PayoutsMoneyTransferResponseModel>PayoutsMoneyTransferResponseAsync(PayoutsMoneyTransferRequestModel requestModel)
        {
            var client = new RestClient(apiUrl);
            client.AddDefaultHeader("Authorization", $"Basic {Base64Encode(apiKey + ":" + secretKey)}");

            var request = new RestRequest("payouts", Method.Post);
            request.AddJsonBody(requestModel);

            var response = await client.ExecuteAsync<PayoutsMoneyTransferResponseModel>(request);

            if (response == null) return new() { error = new Error() { description = "Error!" } };
            return JsonConvert.DeserializeObject<PayoutsMoneyTransferResponseModel>(response.Content)??new();
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
