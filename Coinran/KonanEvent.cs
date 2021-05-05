using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinran
{
    public class MiningEventArgs : EventArgs
    {
        public int Diff { get; set; }
        public string Hash { get; set; }
        public DateTime Date { get; set; }
    }     

    public class ServerEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
