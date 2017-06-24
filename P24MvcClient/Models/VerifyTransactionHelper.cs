using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace P24MvcClient.Models
{
    public class VerifyTransactionHelper
    {
        [Required]
        public string p24_merchant_id { get; set; }
        [Required]
        public string p24_pos_id { get; set; }
        [Required]
        public string p24_session_id { get; set; }
        [Required]
        public string p24_amount { get; set; }
        [Required]
        public string p24_currency { get; set; }
        [Required]
        public string p24_order_id { get; set; }
        [Required]
        public string p24_method { get; set; }
        [Required]
        public string p24_statent { get; set; }
        [Required]
        public string p24_sign { get; set; }
    }
}