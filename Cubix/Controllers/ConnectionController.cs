using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cubix.Filters;
using Cubix.Models;
using Cubix.Utility;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cubix.Controllers
{
    /// <summary>
    /// Connection Controller used for Chat and PING user to user HUB connection
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    // [ServiceFilter(typeof(AdminTokenFilter))]
    public class ConnectionController : BaseController
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Constructor 
        /// </summary>

        public ConnectionController(IHubContext<ChatHub> hubContext, IDistributedCache distributedCache)
        {
            _hubContext = hubContext;
            _distributedCache = distributedCache;
        }


        /// <summary>
        /// Get ConnectionId By Passing UserId
        /// </summary>
        [HttpGet("GetConnection/{Id}")]
        public string GetConnection(string Id)
        {

            return Cache.Get(_distributedCache, Id).ConnectionId;
            // await _hubContext.Clients.Client(Id).SendAsync("GetConnection", "Hello");
        }


        /// <summary>
        /// Send Message
        /// </summary>
        [HttpGet("SendMessageByDoctor")]
        public async void SendMessageByDoctor(string FromId, string ToId, string Message)
        {
            string ConnectionId = Cache.Get(_distributedCache, ToId).ConnectionId;
            await _hubContext.Clients.Client(ConnectionId).SendAsync("MessageReceivedByPatient", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }


        /// <summary>
        /// Send Message
        /// </summary>
        [HttpGet("SendMessageByPatient")]
        public async void SendMessageByPatient(string FromId, string ToId, string Message)
        {
            string ConnectionId = Cache.Get(_distributedCache, ToId).ConnectionId;
            await _hubContext.Clients.Client(ConnectionId).SendAsync("MessageReceivedByDoctor", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }


        /// <summary>
        /// Get ConnectionId By Passing UserId
        /// </summary>
        [HttpGet("TestConnection/{Id}")]
        public async void TestConnection(string Id)
        {
            var connection = Cache.Get(_distributedCache, Id);
            if (connection != null)
                await _hubContext.Clients.Client(Cache.Get(_distributedCache, connection.ConnectionId).ConnectionId).SendAsync("testconnection", "Hello");
        }


        /// <summary>
        /// Call Initiated
        /// </summary>
        [HttpGet("CallInitiated")]
        public async Task<bool> CallInitiated(string FromId, string ToId, string Message, string Name)
        {
            GenerateVCConnectionRoom(FromId);
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
            {
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("CallInitiated", new MessageHub { FromId = FromId, ToId = ToId, Message = Message, Name = Name });
                return true;
            }
            else
            {
                connection = Cache.Get(_distributedCache, FromId);
                if (connection != null)
                    await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("NotAvailable  ", new MessageHub { FromId = ToId, ToId = FromId, Message = "Not Available" });
            }

            return false;
        }


        /// <summary>
        /// MR Call Initiated
        /// </summary>
        [HttpGet("MRCallInitiated")]
        public async Task<bool> MRCallInitiated(string FromId, string ToId, string Message, string Name)
        {
            GenerateVCConnectionRoom(FromId);
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
            {
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("MRCallInitiated", new MessageHub { FromId = FromId, ToId = ToId, Message = Message, Name = Name });
                return true;
            }
            else
            {
                connection = Cache.Get(_distributedCache, FromId);
                if (connection != null)
                    await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("NotAvailable  ", new MessageHub { FromId = ToId, ToId = FromId, Message = "Not Available" });
            }

            return false;
        }
        /// <summary>
        /// Call Initiated
        /// </summary>
        [HttpGet("CallDenied")]
        public async void CallDenied(string FromId, string ToId, string Message)
        {
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("CallDenied", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }

        /// <summary>
        /// MR Call Accepted
        /// </summary>
        [HttpGet("MRCallAccepted")]
        public async void MRCallAccepted(string FromId, string ToId, string Message)
        {
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("MRCallAccepted", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }

        /// <summary>
        /// Call Accepted
        /// </summary>
        [HttpGet("CallAccepted")]
        public async void CallAccepted(string FromId, string ToId, string Message)
        {
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("CallAccepted", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }

        private string GenerateVCConnectionRoom(string Id)
        {
            string RoomName = "Room" + Id;
            var payload = new Dictionary<string, object>()
                    {
                        { "aud", "jitsi" },
                        { "iss", "ecubix-connect-appid" },
                        { "sub", "connect.ecubix.com" },
                        { "room",  RoomName},
                        { "exp", DateTime.Now.AddHours(1).Ticks }
                    };
            var secretKey = Encoding.UTF8.GetBytes("ecubix-connect-app-secret");
            string token = JWT.Encode(payload, secretKey, Jose.JwsAlgorithm.HS256);

            _distributedCache.SetStringAsync(RoomName, token).ConfigureAwait(true);
            return token;
        }


        /// <summary>
        /// getVCtoken
        /// </summary>
        [HttpGet("GetVCToken/{Id}")]
        public async Task<ResultModel<VCConnection>> GetVCToken(string Id)
        {
            ResultModel<VCConnection> oVCConnection = new ResultModel<VCConnection>();
            try
            {
                oVCConnection.Model = new VCConnection();
                string room = "Room" + Id;
                oVCConnection.Model.Token = await _distributedCache.GetStringAsync(room);
                oVCConnection.Model.Room = room;


            }
            catch (Exception)
            {

            }
            return oVCConnection;
        }


        /// <summary>
        /// Doctor Left
        /// </summary>
        [HttpGet("LeaveByDoctor")]
        public async void LeaveByDoctor(string FromId, string ToId, string Message)
        {
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("DoctorLeft", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
        }


        /// <summary>
        /// Consultation Complete
        /// </summary>
        [HttpGet("ConsultationComplete")]
        public async void ConsultationComplete(string FromId, string ToId, string Message)
        {
            string RoomName = "Room" + FromId;
            await _distributedCache.RemoveAsync(RoomName);
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
            {
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("ConsultationComplete", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
            }
        }

        /// <summary>
        /// Consultation Complete
        /// </summary>
        [HttpGet("MRConsultationComplete")]
        public async void MRConsultationComplete(string FromId, string ToId, string Message)
        {
            string RoomName = "Room" + FromId;
            await _distributedCache.RemoveAsync(RoomName);
            var connection = Cache.Get(_distributedCache, ToId);
            if (connection != null)
            {
                await _hubContext.Clients.Client(connection.ConnectionId).SendAsync("MRConsultationComplete", new MessageHub { FromId = FromId, ToId = ToId, Message = Message });
            }
        }
        

    }
}
