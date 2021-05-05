using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Coinran
{
    public class Konan
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string PrevHash { get; set; }
        public string TimeStamp { get; set; }
        public int Difficulty { get; set; }
        public string Nonce { get; set; }
        public KonanTrans Transaction { get; set; }

        public string GetHash()
        {
            string str = this.PrevHash + this.TimeStamp + this.Nonce + this.Transaction.GetHash();

            return BlockChain.Sha3Cal(str);
        }
    }
}
