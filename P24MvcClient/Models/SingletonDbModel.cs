using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przelewy24;

namespace P24MvcClient.Models
{
    public class SingletonDbModel : IP24Db
    {
        private static SingletonDbModel instance;
        private List<Transaction> transactions;

        private SingletonDbModel ()
        {
            this.transactions = new List<Transaction>();
        }
        
        public static SingletonDbModel GetInstace()
        {
            if(SingletonDbModel.instance == null)
            {
                SingletonDbModel.instance = new SingletonDbModel();
            }

            return instance;
        }

        public void SaveTransaction(Transaction transaction)
        {
            this.transactions.Add(transaction);
        }

        public Transaction GetTransaction (string sessionId)
        {
            var result = this.transactions.Select(tr => tr)
                            .Where(tr => tr.P24_session_id == sessionId)
                            .FirstOrDefault();
            return result as Transaction;
        }
    }
}