using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendee.net.Models
{
    public class BulkSmsMessage
    {
        public string from { get; set; }
        public string body { get; set; }
        public List<string> to { get; set; }
    }
}
