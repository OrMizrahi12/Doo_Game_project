using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DooGame
{
    internal class Player
    {
        public Rectangle player;
        public Rect playerHitBox;
        public int playerSpeed = 10;
        public bool playrRigth, playerLeft, playerJump;
        ImageBrush playerImg;
        public Player()
        {
            player = new Rectangle();
            playerImg = new ImageBrush(); 
            playerImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
        }

        public void InitialPlayer(Canvas mainCanvas)
        {
            player.Width = 50; player.Height = 50;
            player.Fill = playerImg;


            Canvas.SetTop(player, 0); Canvas.SetLeft(player, 0);        
            mainCanvas.Children.Add(player);

        }

        public void SetPlayerRects()
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
        }

        public void PlayerMoveController(PhysicsDecor ground)
        {
            Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            if (playrRigth)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            if (playerLeft)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (playerJump)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - 30);      
            }
            
        }
    }
}
