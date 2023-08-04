using System;
using System.Collections.Generic;
using System.Text;

namespace Cubix.Models
{
    public class RequestModel
    {
       

        public string secretKey { get; set; }
        public string appId { get; set; }
        public string orderId { get; set; }
        public string orderAmount { get; set; }
        public string orderCurrency { get; set; }
        public string orderNote { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string notifyUrl { get; set; }
        public string returnUrl { get; set; }
        public string merchantRefundId { get; set; }
        public string refundType { get; set; }
        public string refundAmount { get; set; }
        public string refundNote { get; set; }
        public string referenceId { get; set; }
    }

    public class ResponseModel
    {

        public string status { get; set; }
        public string paymentLink { get; set; }
        public string reason { get; set; }
        public string orderStatus { get; set; }
        public string referenceId { get; set; }
        public string paymentMode { get; set; }
    }
}
