using Newtonsoft.Json;

namespace Zevopay.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BankAccount
    {
        public string Name { get; set; }
        public string Ifsc { get; set; }
        public string Account_number { get; set; }
    }

    public class Contact
    {
        //public string Id { get; set; }

        //public string Entity { get; set; }

        public string Name { get; set; }

        public string contact { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public string Reference_id { get; set; }

        //public string Batch_id { get; set; }

        //public bool Active { get; set; }

        public Notes Notes { get; set; }

        //public int Created_at { get; set; }
    }
    public class Error
    {
        public string Source { get; set; }

        public string Reason { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Step { get; set; }

        public Metadata Metadata { get; set; }
    }


    public class FundAccount
    {
        //public string Id { get; set; }

        //public string Entity { get; set; }

        //public string Contact_id { get; set; }

        public Contact Contact { get; set; }

        public string Account_type { get; set; }

        public BankAccount Bank_account { get; set; }

        //public Vpa Vpa { get; set; }

        //public string Batch_id { get; set; }

        //public bool Active { get; set; }

        //public int Created_at { get; set; }
    }

    public class FundAccountUPI
    {
        //public string Id { get; set; }

        //public string Entity { get; set; }

        //public string Contact_id { get; set; }

        public Contact Contact { get; set; }

        public string Account_type { get; set; }

        public Vpa Vpa { get; set; }

        //public string Batch_id { get; set; }

        //public bool Active { get; set; }

        //public int Created_at { get; set; }
    }

    public class Metadata
    {
    }

    public class Notes
    {
        public string Notes_key_1 { get; set; }

        public string Notes_key_2 { get; set; }
    }

    public class PayoutsMoneyTransferRequestModel
    {
        public string Account_number { get; set; }

        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Mode { get; set; }
        public string Purpose { get; set; }
        public FundAccount Fund_account { get; set; }
        public bool Queue_if_low_balance { get; set; }
        public string Reference_id { get; set; }
        public string Narration { get; set; }
        public Notes Notes { get; set; }
    }


    public class PayoutsMoneyTransferResponseModel
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public string Fund_account_id { get; set; }
        public FundAccount Fund_account { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public Notes Notes { get; set; }
        public int Fees { get; set; }
        public int Tax { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string Utr { get; set; }
        public string Mode { get; set; }
        public string Reference_id { get; set; }
        public string Narration { get; set; }
        public string Batch_id { get; set; }
        public string Failure_reason { get; set; }
        public int Created_at { get; set; }
        public string Fee_type { get; set; }
        public StatusDetails status_details { get; set; }
        public string merchant_id { get; set; }
        public string status_details_id { get; set; }
        public Error Error { get; set; }
    }
    public class StatusDetails
    {
        public string Reason { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
    }

    public class UpiPayoutRequestModel
    {
        public string Account_number { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Mode { get; set; }
        public string Purpose { get; set; }
        public FundAccountUPI Fund_account { get; set; }
        public bool Queue_if_low_balance { get; set; }
        public string Reference_id { get; set; }
        public string Narration { get; set; }
        public Notes Notes { get; set; }
    }
    public class UpiPayoutResponseModel
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public string Fund_account_id { get; set; }
        public FundAccountUPI Fund_account { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public Notes Notes { get; set; }
        public int Fees { get; set; }
        public int Tax { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string Utr { get; set; }
        public string Mode { get; set; }
        public string Reference_id { get; set; }
        public string Narration { get; set; }
        public string Batch_id { get; set; }
        public string Failure_reason { get; set; }
        public int Created_at { get; set; }
        public string Fee_type { get; set; }
        public StatusDetails Status_details { get; set; }
        public string Merchant_id { get; set; }
        public string Status_details_id { get; set; }
        public Error Error { get; set; }

    }

    public class Vpa
    {
        public string Username { get; set; }
        public string Handle { get; set; }
        public string Address { get; set; }
    }


}
