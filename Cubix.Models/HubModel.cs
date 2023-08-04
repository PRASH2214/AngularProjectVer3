using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class HubModel
    {
        public  string ConnectionId { get; set; }
        public string MemberId { get; set; }
        public string MemberKey { get; set; }
    }


    public class MessageHub
    {

        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
    }


    public class VCConnection
    {
        public string Room { get; set; }
        public string Token { get; set; }
    }
}
