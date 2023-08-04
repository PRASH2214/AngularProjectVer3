using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix
{
    public interface IChatHub
    {
        //So this method is a JS one not a .net one and will be called on the client(s)

        public string GetConnectionId();
       // Task SendMessage(string user, string message);
    }
}
