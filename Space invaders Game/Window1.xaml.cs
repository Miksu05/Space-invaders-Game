using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Space_invaders_Game
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class Window1 : Window
    {
        SoundPlayer testPlayer;
        public Window1()
        {
            InitializeComponent();

            testPlayer = new SoundPlayer(Environment.CurrentDirectory + "\\Sound\\MAin_MEnu_musiikki.wav");
            testPlayer.Load();

            
            testPlayer.Play();  
        }





        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            testPlayer.Stop();

            MainWindow Main1 = new MainWindow();
            Main1.Show();
            this.Close();

        }

        private void OpenWindowQuit(object sender, RoutedEventArgs e)
        {
            testPlayer.Stop();

            Environment.Exit(1);
        }

        private void OpenWindowOptions(object sender, RoutedEventArgs e)
        {
            testPlayer.Stop();

            Window2 Main3 = new Window2();
            Main3.Show();
            this.Close();
        }
    }
}
