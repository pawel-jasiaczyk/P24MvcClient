﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Przelewy24;

namespace P24MvcClient.Models
{
    public class OnePageClientViewModel
    {
        private static int numberOfInstanes = 0;
        private static OnePageClientViewModel singletonInstance;
        
        public Przelewy24.Przelewy24 P24 { get; set; }
        public Przelewy24.Transaction Transaction { get; set; }

        public bool AutomaticRedirection { get; set; }

        public int InstanceNumber { get; private set; }

        private OnePageClientViewModel(bool insideCall)
        {
            if(insideCall)
            {
                AllConstructorsOpetarions();
                // Test Data - to Delete
                numberOfInstanes++;
                this.InstanceNumber = numberOfInstanes;
            }
        }

        public OnePageClientViewModel()
        {
            if(OnePageClientViewModel.singletonInstance != null)
            {
                this.P24 = OnePageClientViewModel.singletonInstance.P24;
                this.Transaction = new Transaction(this.P24);
            }
            else
            {
                AllConstructorsOpetarions();
                OnePageClientViewModel.singletonInstance = this;
            }
            // Test Data - to Delete
            numberOfInstanes++;
            this.InstanceNumber = numberOfInstanes;
        }

        private void AllConstructorsOpetarions()
        {
            this.P24 = new Przelewy24.Przelewy24();
            this.P24.P24_url_status = HttpContext.Current.Request.Url.Authority + "/home/P24Status";
#if DEBUG
            this.P24.MerchantId = 1604;
            this.P24.PosId = 1604;
            this.P24.CrcKey = "4aca0b5e3950f78b";
            this.P24.SandboxMode = true;
#endif
            this.Transaction = new Transaction(this.P24);
            this.P24.TransactionDb = SingletonDbModel.GetInstace() as IP24Db;
        }

        public static OnePageClientViewModel GetInstance()
        {
            if(OnePageClientViewModel.singletonInstance == null)
            {
                OnePageClientViewModel.singletonInstance = new OnePageClientViewModel(true);
            }

            return OnePageClientViewModel.singletonInstance;
        }
    }
}