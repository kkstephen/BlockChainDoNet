using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinran
{
    public class KonanTrans
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public decimal Amounts { get; set; }
        public decimal Fee { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public string GetHash()
        {
            return BlockChain.Sha3Cal(this.Sender + this.Receiver + this.Amounts.ToString() + this.Fee.ToString() + this.Date.ToString() + this.Message);
        }
    }
}
