using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Coinran
{
    public sealed class BlockChain
    {
        public IList<Konan> List { get; private set; }

        public int Total { get; set; }
     
        public static BlockChain Current { get; } = new BlockChain();

        public BlockChain()
        {
            this.List = new List<Konan>();
        }

        public Konan Create(string data, string prevhash, int diff)
        {
            Konan konan = new Konan()
            {
                Id = this.List.Count + 1,
                PrevHash = prevhash,
                TimeStamp = DateTime.Now.Ticks.ToString(),
                Nonce = "0",
                Difficulty = diff,
                Transaction = new KonanTrans()
            };

            return konan;
        }

        public void Add(Konan konan, int nonce)
        {
            konan.Nonce = nonce.ToString();
            konan.Hash = konan.GetHash();

            this.List.Add(konan);
        } 

        public bool IsValid(string hash, string target)
        {
            return hash.Substring(0, target.Length) == target;
        }

        public static string Sha3Cal(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            return Sha3Cal(bytes);
        }

        public static string Sha3Cal(byte[] buffer)
        {
            var hash = new StringBuilder();

            using (SHA256 sha3 = SHA256.Create())
            {                 
                byte[] crypto = sha3.ComputeHash(buffer);

                foreach (byte b in crypto)
                {
                    hash.Append(b.ToString("x2"));
                }
            }

            return hash.ToString();
        } 
    }
}
