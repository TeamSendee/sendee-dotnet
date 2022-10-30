using Sendee.net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendee.net
{
    public class SendeeClient
    {
        private string _apiKey;
        public SendeeClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> SendMessage(string to, string from, string body)
        {
            SendMessage sendMessage = new SendMessage(_apiKey);
            string response = await sendMessage.SendSingle(to, from, body);

            return response;
        }

        public async Task<string> SendBulkMessage(List<string> to, string from, string body)
        {
            SendMessage sendMessage = new SendMessage(_apiKey);
            string response = await sendMessage.SendBulk(to, from, body);

            return response;
        }
    }
}
