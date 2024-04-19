using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Json;
using Zevopay.Models;

namespace Zevopay.Controllers.API
{
    [Route("api/Callback")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        [HttpGet("GetVirtual")]
        public IActionResult Virtual(int id)
        {
            return Ok(new string[] {"sdgd","asger","fgh"});
        }
        [HttpPost("payout")]
        public async Task<IActionResult> PayoutCallBack()
        {
            string requestBody;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                requestBody =await reader.ReadToEndAsync();
            }
            Root ResponseCallBackModel =
                JsonSerializer.Deserialize<Root>(requestBody);

            var entity = ResponseCallBackModel.entity;
            var created_at = ResponseCallBackModel.created_at;
            var events = ResponseCallBackModel.@event;
            var payload = ResponseCallBackModel.payload.payout.entity;
            var account_id = ResponseCallBackModel.account_id;
            var amount = payload.amount;
            var id = payload.id;
            var fund_account_id = payload.fund_account_id;
            var status = payload.status;
            var utr = payload.utr;
            var mode = payload.mode;
            var reference_id = payload.reference_id;
            var narration = payload.narration;
            var status_details = payload.status_details;
            var description = payload.status_details.Description;

            //Console.WriteLine($"Date: {ResponseCallBackModel?.entity}");
            //Console.WriteLine($"TemperatureCelsius: {ResponseCallBackModel?.TemperatureCelsius}");
           // Console.WriteLine($"Summary: {ResponseCallBackModel?.Summary}");

            // Now requestBody contains the entire JSON or other data from the request body
            // You can process it as needed

            return Ok(new string[] {"ok"});
        }


        [HttpPost]
        public ActionResult ProcessData()
        {
            string requestBody;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                requestBody = reader.ReadToEnd();
            }
            // Process raw body
            return null;
        }
    }
}
