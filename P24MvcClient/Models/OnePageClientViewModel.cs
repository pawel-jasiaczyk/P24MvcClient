using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przelewy24;

namespace P24MvcClient.Models
{
    public class OnePageClientViewModel
    {
        public Przelewy24.Przelewy24 P24 { get; set; }
        public Przelewy24.Transaction Transaction { get; set; }

        public OnePageClientViewModel()
        {
            this.Transaction = new Transaction();
            this.P24 = new Przelewy24.Przelewy24();
        }
    }
}