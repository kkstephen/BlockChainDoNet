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
            this.log_text("start mining: the first block");

            BlockChain.Current.Total = int.Parse(this.txTotal.Text);

            BlockServer server = new BlockServer();

            server.Difficulty = 1;

            server.OnMiningSuccess += Server_OnMiningSuccess;
            server.OnMiningEnd += Server_OnMiningEnd;

            server.start();
        }

        private void Server_OnMiningEnd(object sender, ServerEventArgs e)
        {
            this.log_text(e.Message);
        }

        private void Server_OnMiningSuccess(object sender, MiningEventArgs e)
        {             
            this.log_text(e.Date + " d:" + e.Diff + " h: " + e.Hash);            
        }

        private void log_text(string msg)
        {
            this.Dispatcher.Invoke(() => {
                this.txt_box.AppendText(msg + "\r\n");
            });
        }
    }
}
