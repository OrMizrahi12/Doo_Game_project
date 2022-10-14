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
    internal class PhysicsDecor
    {
        public Rectangle ground1, ground2, ground3 ,background1, background2;
        public Rect groundRect1, groundRect2, groundRect3;
        ImageBrush background ,ground;
        public PhysicsDecor()
        {
            ground1 = new Rectangle();  
            ground2 = new Rectangle();
            ground3 = new Rectangle();
            background1 = new Rectangle();
            background2 = new Rectangle();
            background = new ImageBrush();
            ground = new ImageBrush();
        }

        public void InitialDecor(Canvas mainCanvas)
        {
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/H.jpg"));
            ground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ground4.png"));

            background1.Width = 1262; background2.Width = 1262;
            background1.Height = 400; background2.Height = 400;
            ground1.Width = 250; ground2.Width = 250; ground3.Width = 250;
            ground1.Height = 40; ground2.Height = 40; ground3.Height = 40;

            background1.Fill = background; background2.Fill = background;           
            ground1.Fill = ground; ground2.Fill = ground; ground3.Fill = ground;

            Canvas.SetTop(ground1, 200); Canvas.SetLeft(ground1, 0);
            Canvas.SetTop(ground2, 300); Canvas.SetLeft(ground2, 250);
            Canvas.SetTop(ground3, 150); Canvas.SetLeft(ground3, 500);
            Canvas.SetLeft(background1, 0); Canvas.SetLeft(background2, 1262);

            mainCanvas.Children.Add(background1); mainCanvas.Children.Add(background2);
            mainCanvas.Children.Add(ground1); mainCanvas.Children.Add(ground2); mainCanvas.Children.Add(ground3);
        }

        public void SetGroundRects()
        {
            groundRect1 = new Rect(Canvas.GetLeft(ground1), Canvas.GetTop(ground1), ground1.Width - 15, ground1.Height);
            groundRect2 = new Rect(Canvas.GetLeft(ground2), Canvas.GetTop(ground2), ground2.Width - 15, ground2.Height);
            groundRect3 = new Rect(Canvas.GetLeft(ground3), Canvas.GetTop(ground3), ground3.Width - 15, ground3.Height);
        }

        public void GroundPhysics(Player player)
        {
            if (groundRect1.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground1) - ground1.Height) ||
                groundRect2.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground2) - ground2.Height) ||
                groundRect3.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground3) - ground3.Height)
                )
            {
                Canvas.SetTop(player.player, Canvas.GetTop(player.player) - player.playerSpeed);
            }
        }

        public void NoveScreen(Player player )
        {
            if (Canvas.GetLeft(player.player) > 300 && player.playrRigth == true)
            {
                Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 10);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 10);
                Canvas.SetLeft(ground1, Canvas.GetLeft(ground1) - 10);
                Canvas.SetLeft(ground2, Canvas.GetLeft(ground2) - 10);
                Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) - 10);
                Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) - 10);
            }
            else if(Canvas.GetLeft(player.player) < 200 && player.playerLeft == true)
            {
                Canvas.SetLeft(background1, Canvas.GetLeft(background1) + 10);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) + 10);
                Canvas.SetLeft(ground1, Canvas.GetLeft(ground1) + 10);
                Canvas.SetLeft(ground2, Canvas.GetLeft(ground2) + 10);
                Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) + 10);
                Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) + 10);
            }
        }

        public void GroundLocator(Player player)
        {
            if (player.playrRigth)
                if (Canvas.GetLeft(ground1) < -200)
                    Canvas.SetLeft(ground1, 600);
                else if (Canvas.GetLeft(ground2) < -200)
                    Canvas.SetLeft(ground2, 600);
                else if (Canvas.GetLeft(ground3) < -200)
                    Canvas.SetLeft(ground3, 600);
            if (player.playerLeft)
                if (Canvas.GetLeft(ground1) > 600) 
                    Canvas.SetLeft(ground1, -200);
                else if (Canvas.GetLeft(ground2) > 600)
                    Canvas.SetLeft(ground2, -200);
                else if (Canvas.GetLeft(ground3) > 600)     
                    Canvas.SetLeft(ground3, -200);
        }

        public void MakeBackGroundInfinity()
        {
            if (Canvas.GetLeft(background1) < -background1.Width)          
                Canvas.SetLeft(background1, background2.Width - 15);            
            if (Canvas.GetLeft(background2) < -background2.Width)          
                Canvas.SetLeft(background2, background1.Width - 15);
            if (Canvas.GetLeft(background2) > background2.Width)
                Canvas.SetLeft(background2, -background1.Width + 15);
            if (Canvas.GetLeft(background1) > background1.Width)
                Canvas.SetLeft(background1, -background1.Width + 15);
        }
    }
}
