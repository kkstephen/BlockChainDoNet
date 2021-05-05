using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinran
{
    public class BlockServer
    {
        public event EventHandler<MiningEventArgs> OnMiningSuccess;
        public event EventHandler<ServerEventArgs> OnMiningEnd;

        public int Difficulty { get; set; }

        public Queue<KonanTrans> Transactions { get; set; }

        public BlockServer()
        {
            this.Transactions = new Queue<KonanTrans>();
        }

        public void addTransaction(Konan block)
        {
            if (this.Transactions.Count > 0)
            {
                block.Transaction = this.Transactions.Dequeue();
            }
        }

        public void start()
        { 
            Konan block = BlockChain.Current.Create("The first block: Hello world!", "0", this.Difficulty);
 
            //start mining
            Task.Run(() =>
            {
                for (int i = 0; i < BlockChain.Current.Total; i++)
                {
                    if (i % 10 == 0 && i >= 10) this.Difficulty++;
                     
                    int nonce = this.mining(block);

                    this.addTransaction(block);

                    BlockChain.Current.Add(block, nonce);

                    if (OnMiningSuccess != null)
                    {
                        OnMiningSuccess(this, new MiningEventArgs() { Diff = this.Difficulty, Hash = block.Hash, Date = DateTime.Now });
                    }

                    block = BlockChain.Current.Create("block no." + i, BlockChain.Current.List[i].Hash, this.Difficulty);
                    
                    Task.Delay(200);
                }

                if (OnMiningEnd != null)
                {
                    OnMiningEnd(this, new ServerEventArgs() { Message = "Server mining end." });
                }
            });             
        }

        public int mining(Konan konan)
        {
            int n = 0;

            string target = new string('0', this.Difficulty);

            while (!BlockChain.Current.IsValid(konan.GetHash(), target))
            {
                n++;

                konan.Nonce = n.ToString();
            }

            return n;
        }
    }
}
