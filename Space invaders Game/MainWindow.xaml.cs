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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Space_invaders_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Window1 Main2 = new Window1();
        

        SoundPlayer testPlayer;
        //MediaPlayer musicPlayer;
        //SpaceGame game;
        DispatcherTimer GameTimer;
        double deltaTime;




        bool goLeft, goRight;

        List<Rectangle> itemsToRemove = new List<Rectangle>();
        Window4 Window4;

        int enemyImages = 0;
        int bulletTimer = 0;
        int bulletTimerLimit = 40;
        int totalEnemies = 0;
        int enemySpeed = 6;
        bool Options = false;
        bool gameOver = false;
        Random random = new Random();

        public DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();
        //MediaPlayer sound = new MediaPlayer();
        //Uri woopUri = new Uri(Environment.CurrentDirectory + @"\Sound\shoot.mp3");
        

        public MainWindow()
        {


            InitializeComponent();


            testPlayer = new SoundPlayer(Environment.CurrentDirectory + "\\Sound\\playerShoot.wav");
            testPlayer.Load();

            //musicPlayer = new MediaPlayer();
            //musicPlayer.Open(new Uri(Environment.CurrentDirectory + "\\audio\\MenuMusic.mp3"));

            gameTimer = new DispatcherTimer();

            deltaTime = 1.0 / 30.0;
            gameTimer.Interval = TimeSpan.FromSeconds(deltaTime);
            

            //game = new SpaceGame();
            //game.Start(myCanvas);
            gameTimer.Start();





            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            player.Fill = playerSkin;
            //sound.Open(woopUri);

            myCanvas.Focus();

            makeEnemies(8);


        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (Window4.NOdeath == true)
            {
                Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            }
            else
            {
                return;
            }
            
            enemiesLeft.Content = "Enemies Left: " + totalEnemies;

            if (goLeft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            }
            if ( goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
            }


            bulletTimer -= 3;

            if (bulletTimer < 0)
            {
                List <Rectangle> enemies = new List<Rectangle>();
                foreach (var x in myCanvas.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "enemy")
                    {
                        enemies.Add(x);
                    }
                }
                Rectangle randomenemy = enemies[random.Next(0, enemies.Count)];
                    enemyBulletMaker(Canvas.GetLeft(randomenemy) + 20, 10);

                bulletTimer = bulletTimerLimit;
            }
            bool enemyHitWall = false;

            

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {

                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bullet.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                                totalEnemies -= 1;
                                   //lisää ääni

                            }
                        }
                    }




                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);
                    //Canvas.SetRight(x, Canvas.GetRight(x) + enemySpeed);

                    if (Canvas.GetLeft(x) > 820)
                    {
                        enemyHitWall = true;
                        
 
                    }
                    if (Canvas.GetLeft(x) < 0)
                    {
                        enemyHitWall = true;

                        
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);



                }
                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x,Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

  
                }
            }
            if (enemyHitWall == true)
            {
                enemySpeed = enemySpeed *- 1;

                foreach (var x in myCanvas.Children.OfType<Rectangle>())
                {
                    if ((string)x.Tag == "enemy")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }
                }

                // käyläpi joka viholliset siirtää jokaista alas  

            }

            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            if (totalEnemies < 10)
            {
                //enemySpeed = 12;
            }

            if ( totalEnemies <= 0)
            {
                showGameOver("You win, you saved the world!");
                //lisää ääni
            }
        }




        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
                //lisää ääni vasemmalle
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
                //lisää ääni oikealle
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }

            if (e.Key == Key.Space)
            {
                testPlayer.Play();




                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red,
                    
                };
                
                
                
                Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);

                myCanvas.Children.Add(newBullet);



            }

            if (e.Key == Key.Enter && gameOver == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        

        public void enemyBulletMaker(double x, double y)
        {
            if (Window4.NOENEMIES == true)
            {

                Rectangle newEnemyBullet = new Rectangle
                {

                    Tag = "enemyBullet",
                    Height = 40,
                    Width = 15,
                    Fill = Brushes.Yellow,
                    Stroke = Brushes.Black,
                    StrokeThickness = 5

                };


                Canvas.SetTop(newEnemyBullet, y);
                Canvas.SetLeft(newEnemyBullet, x);

                myCanvas.Children.Add(newEnemyBullet);


            }
            else
            {
                return;
            }





        }

        public void makeEnemies(int limit)
        {
            if (Window4.NOENEMIES == true)
            { 
        

                totalEnemies = limit;
                for (int rivi = 0; rivi < 4; rivi++)
                {
                    int top = 10 + rivi * 90;
                    int left = 10;

                    for (int i = 0; i < limit; i++)
                    {

                        ImageBrush enemySkin = new ImageBrush();

                        Rectangle newEnemy = new Rectangle
                        {
                            Tag = "enemy",
                            Height = 45,
                            Width = 45,
                            Fill = enemySkin
                        };

                        Canvas.SetTop(newEnemy, top);
                        Canvas.SetLeft(newEnemy, left);
                        myCanvas.Children.Add(newEnemy);
                        left += 60;
                    


                        enemyImages++;

                        if (enemyImages > 8)
                        {
                            enemyImages = 1;
                        }

                        switch (enemyImages)
                        {
                            case 1:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader1.gif"));
                                break;
                            case 2:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader2.gif"));
                                break;
                            case 3:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader3.gif"));
                                break;
                            case 4:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader4.gif"));
                                break;
                            case 5:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader5.gif"));
                                break;
                            case 6:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader6.gif"));
                                break;
                            case 7:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader7.gif"));
                                break;
                            case 8:
                                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/invader8.gif"));
                                break;

                        }
                    }



                }
            }
            else
            {
                return;
            }
        }
        private void showGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();   

            Window1 win1= new Window1();
            win1.Show();
            this.Close();
        
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Options = true;
                gameTimer.Stop();



                Window3 window3 = new Window3();
                window3.Show();
                this.Close();
            }
        }
    }
}
