using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coinran;

namespace KonanCoin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.tbLable.Text = BlockChain.Sha3Cal(this.tbInput.Text);
        }

        private void BtnMine_Click(object sender, RoutedEventArgs e)
        {
            BlockChain.Current.Difficulty = 1;
            BlockChain.Current.Total = int.Parse(this.txTotal.Text);

            Konan block = BlockChain.Current.Create("The first block: Hello world!", "0");

            this.txt_box.AppendText("\r\n");
            this.txt_box.AppendText("Create first block.\r\n"); 

            //start mining
            Task.Run(() =>
            {
                for (int i = 0; i < BlockChain.Current.Total; i++)
                {
                    if (i % 10 == 0 && i >= 10) BlockChain.Current.Difficulty++;

                    int nonce = this.mining(block);

                    BlockChain.Current.Add(block, nonce);

                    block = BlockChain.Current.Create("block no." + i, BlockChain.Current.List[i].Hash);

                    this.Dispatcher.InvokeAsync(() =>
                    {
                        this.txt_box.AppendText(block.TimeStamp + " d:" + block.Difficulty + " n:" + nonce + " " + block.PrevHash + "\r\n");
                    });

                    Task.Delay(200);
                }
            });


            this.txt_box.AppendText(DateTime.Now.ToString() + ": done.");
        }

        public int mining(Konan konan)
        {
            int n = 0;

            string target = new string('0', BlockChain.Current.Difficulty);

            while (!BlockChain.Current.IsValid(konan.GetHash(), target))
            {
                n++;

                konan.Nonce = n.ToString();            
            }

            return n;
        }
    }
}
