using Newtonsoft.Json;

namespace Zevopay.Models
{
    public class ResponseCallBackModel
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Entity
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string fund_account_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
       // public Notes notes { get; set; }
        public int fees { get; set; }
        public int tax { get; set; }
        public string status { get; set; }
        public string purpose { get; set; }
        public object utr { get; set; }
        public string mode { get; set; }
        public object reference_id { get; set; }
        public string narration { get; set; }
        public object batch_id { get; set; }
        public StatusDetails status_details { get; set; }
        public int created_at { get; set; }
        public string fee_type { get; set; }
    }

    

    public class Payload
    {
        public Payout payout { get; set; }
    }

    public class Payout
    {
        public Entity entity { get; set; }
    }

    public class Root
    {
        public string entity { get; set; }
        public string account_id { get; set; }
        public string @event { get; set; }
        public List<string> contains { get; set; }
        public Payload payload { get; set; }
        public int created_at { get; set; }
    }

   


}
