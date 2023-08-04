using Cubix.Models;
using Cubix.Utility;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix
{

    /// <summary>
    /// Hub Class for Chat and making call signals from one user to another
    /// </summary>
    public class ChatHub : Hub<IChatHub>
    {
        private object ParticipantsConnectionLock = new object();

        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Chat Hub Constructor
        /// </summary>
        public ChatHub(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        /// <summary>
        /// Established Doctor Connection
        /// </summary>
        public void UserJoin(string DoctorId, string message)
        {
            HubModel oHubModel = new HubModel();
            oHubModel.ConnectionId = Context.ConnectionId;
            oHubModel.MemberKey = DoctorId;
            oHubModel.MemberId = DoctorId;
            Cache.Set(_distributedCache,oHubModel.MemberKey, oHubModel).ConfigureAwait(true);
            Cache.Set(_distributedCache, oHubModel.ConnectionId, oHubModel).ConfigureAwait(true);

        }

        ///// <summary>
        ///// Established Patient Connection
        ///// </summary>
        //public void PatientJoin(string DoctorId, string message)
        //{
        //    HubModel oHubModel = new HubModel();
        //    oHubModel.ConnectionId = Context.ConnectionId;
        //    oHubModel.MemberKey = DoctorId;
        //    oHubModel.MemberId = DoctorId;
        //    Cache.Set(_distributedCache, oHubModel.MemberKey, oHubModel).ConfigureAwait(true);
        //    Cache.Set(_distributedCache, oHubModel.ConnectionId, oHubModel).ConfigureAwait(true);
        //}


        ///// <summary>
        ///// Established MR Connection
        ///// </summary>
        //public void MRJoin(string DoctorId, string message)
        //{
        //    HubModel oHubModel = new HubModel();
        //    oHubModel.ConnectionId = Context.ConnectionId;
        //    oHubModel.MemberKey = DoctorId;
        //    oHubModel.MemberId = DoctorId;
        //    Cache.Set(_distributedCache, oHubModel.MemberKey, oHubModel).ConfigureAwait(true);
        //    Cache.Set(_distributedCache, oHubModel.ConnectionId, oHubModel).ConfigureAwait(true);
        //}


        /// <summary>
        /// Calls When connection established
        /// </summary>       
        public override Task OnConnectedAsync()
        {
            lock (ParticipantsConnectionLock)
            {
                var connectionIndex = Cache.Get(_distributedCache,Context.ConnectionId.ToString());

                if (connectionIndex != null)
                {
                    Cache. Set(_distributedCache,connectionIndex.MemberKey.ToString(), connectionIndex).ConfigureAwait(true);
                }

                return base.OnConnectedAsync();
            }
        }

        /// <summary>
        /// Calls When connection disconnected
        /// </summary>
        public override Task OnDisconnectedAsync(Exception exception)
        {

            lock (ParticipantsConnectionLock)
            {
                var connectionIndex = Cache.Get(_distributedCache, Context.ConnectionId.ToString());

                if (connectionIndex != null)
                {
                    Cache.Remove(_distributedCache,connectionIndex.MemberKey.ToString()).ConfigureAwait(false); 
                }

                return base.OnDisconnectedAsync(exception);
            }
        }



    }
}
