using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_invaders_Game
{
    internal class SpaceGame
    {
        Rectangle playerRect;
        double playerSpeed = 140.0;


        public SpaceGame()
        {
            playerRect = new Rectangle();
            playerRect.Stroke = Brushes.Red;
            playerRect.Fill = Brushes.Red;
            playerRect.Height = 40;
            playerRect.Width = 40;
        }

        public void Start(Canvas gameCanvas)
        {
            Canvas.SetTop(playerRect, 10.0);
            Canvas.SetLeft(playerRect, 10.0);
            gameCanvas.Children.Add(playerRect);
        }

        bool isKeyDown(Key key)
        {
            return ((Keyboard.GetKeyStates(key) & KeyStates.Down) > 0);
        }

        public void Update(double deltaTime)
        {
            double y = Canvas.GetTop(playerRect);
            double x = Canvas.GetLeft(playerRect);
            if (isKeyDown(Key.Left))
            {
                Canvas.SetLeft(playerRect, x - deltaTime * playerSpeed);
            }
            else if (isKeyDown(Key.Right))
            {
                Canvas.SetLeft(playerRect, x + deltaTime * playerSpeed);
            }

            if (isKeyDown(Key.Up))
            {
                Canvas.SetTop(playerRect, y - deltaTime * playerSpeed);

            }
            else if (isKeyDown(Key.Down))
            {
                Canvas.SetTop(playerRect, y + deltaTime * playerSpeed);
            }
        }

        public void Draw()
        {

        }
    }
}

