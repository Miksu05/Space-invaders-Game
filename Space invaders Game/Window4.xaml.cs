using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_invaders_Game
{



    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public bool NOENEMIES = true;

        public bool NOdeath = true;

        MainWindow mainWindow;
        public Window4()
        {
            InitializeComponent();
        }

        private void NODEATH(object sender, RoutedEventArgs e)
        {
            NOdeath = false;
        }

        private void Noenemies(object sender, RoutedEventArgs e)
        {
            NOENEMIES = false;
        }

        private void BACKK(object sender, RoutedEventArgs e)
        {
            Window1 main1 = new Window1();  
            main1.Show();
            this.Close();
        }

        private void RESETGAME(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
