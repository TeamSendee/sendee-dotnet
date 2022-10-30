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

                SMSMessage smsMessage = new SMSMessage();
                smsMessage.From = from;
                smsMessage.Body = body;
                smsMessage.To = to;

                return await SendRequest(smsMessage, "sms/send");
                
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                errors.Add("Success", "false");
                errors.Add("Message", "An error has occured. " + ex.Message.ToString());

                return JsonConvert.SerializeObject(errors);
            }
        }

        public async Task<string> SendBulk(string to, string from, string body)
        {
            try
            {
                SMSMessage smsMessage = new SMSMessage();
                smsMessage.From = from;
                smsMessage.Body = body;
                smsMessage.To = to;

                return await SendRequest(smsMessage, "sms/bulk/send");
            }
            catch (Exception ex)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                errors.Add("Success", "false");
                errors.Add("Message", "An error has occured. " + ex.Message.ToString());

                return JsonConvert.SerializeObject(errors);
            }
        }

        public async Task<string> SendRequest(SMSMessage request, string url)
        {
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);


            var response = await client.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
