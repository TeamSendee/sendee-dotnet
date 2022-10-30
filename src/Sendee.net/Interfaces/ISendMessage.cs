using Sendee.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendee.net.Interfaces
{
    public interface ISendMessage
    {
        Task<string> SendSingle(string to, string from, string body);
        Task<string> SendBulk(List<string> to, string from, string body);
    }
}
