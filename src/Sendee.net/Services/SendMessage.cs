using Newtonsoft.Json;
using Sendee.net.Interfaces;
using Sendee.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sendee.net.Services
{
    public class SendMessage : ISendMessage
    {
        private string _apiKey;
        private string _baseUrl;

        public SendMessage(string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = "https://gosendee.com/api/";
        }

        public async Task<string> SendSingle(string to, string from, string body)
        {
            try
            {
                SmsMessage smsMessage = new SmsMessage();
                smsMessage.from = from;
                smsMessage.body = body;
                smsMessage.to = to;

                string json = JsonConvert.SerializeObject(smsMessage);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);


                var response = await client.PostAsync("sms/send", content);

                return await response.Content.ReadAsStringAsync();
                
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                errors.Add("Success", "false");
                errors.Add("Message", "An error has occured. " + ex.Message.ToString());

                return JsonConvert.SerializeObject(errors);
            }
        }

        public async Task<string> SendBulk(List<string> to, string from, string body)
        {
            try
            {
                BulkSmsMessage bulkSmsMessage = new BulkSmsMessage();
                bulkSmsMessage.from = from;
                bulkSmsMessage.body = body;
                bulkSmsMessage.to = to;

                string json = JsonConvert.SerializeObject(bulkSmsMessage);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);


                var response = await client.PostAsync("sms/bulk/send", content);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                errors.Add("Success", "false");
                errors.Add("Message", "An error has occured. " + ex.Message.ToString());

                return JsonConvert.SerializeObject(errors);
            }
        }
    }
}
