using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DooGame
{
    internal class GiftPlayer
    {
        public Rectangle life;
        public Rect lifeRect;
        public ImageBrush lifeImg; 
        public DispatcherTimer BeatingHeartTimer;
        bool increase;
        double counterForHeartBeats = 1;
        Rectangle[] arrOfGrounds = new Rectangle[3];
        Random random = new Random();
        int randomGround;

        public GiftPlayer()
        {
            life = new Rectangle();
            lifeRect = new Rect();
            lifeImg = new ImageBrush();
            BeatingHeartTimer = new DispatcherTimer();
        }
        public void InitialGifts(Canvas mainCanvas, PhysicsDecor decor)
        {
            life.Width = 40; life.Height = 40;
            lifeImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/life_for_player/life_for_player1.png"));
            life.Fill = lifeImg; life.Visibility = Visibility.Collapsed;
            Canvas.SetTop(life,0); Canvas.SetLeft(life, 0);

            mainCanvas.Children.Add(life);

            BeatingHeartTimer.Tick += heartAnimationEngine;
            BeatingHeartTimer.Interval = TimeSpan.FromMilliseconds(50);
            BeatingHeartTimer.Start();
        }

        private void heartAnimationEngine(object sender, EventArgs e)
        {
            ChangeheartAnimation(counterForHeartBeats);
            
            if (counterForHeartBeats == 6)
            {
                increase = false;
            }
            else if (counterForHeartBeats == 0)
            {
                increase = true;
            }
            if (increase)
            {
                counterForHeartBeats+=1;
            }
            else if (!increase)
            {
                counterForHeartBeats-=1;
            }
        }

        public void ChangeheartAnimation(double x)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (i == x)
                {
                    lifeImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/life_for_player/life_for_player{i}.png"));
                }
            }
            life.Fill = lifeImg;
        }

        public void PlayerGiftTouchEvent(Player player, PhysicsDecor decor)
        {
            arrOfGrounds[0] = decor.ground1; arrOfGrounds[1] = decor.ground2; arrOfGrounds[2] = decor.ground3;
            randomGround = random.Next(1, 3);

            if (player.lifePlayer.Value <= 20 && life.Visibility == Visibility.Collapsed)
            {
                life.Visibility = Visibility.Visible;
                Canvas.SetTop(life, Canvas.GetTop(arrOfGrounds[randomGround]) - 35); Canvas.SetLeft(life, Canvas.GetLeft(arrOfGrounds[randomGround]) + (arrOfGrounds[randomGround].Width / 2));
            }
            if (player.playerHitBox.IntersectsWith(lifeRect))
            {
                player.lifePlayer.Value = 100;
                life.Visibility = Visibility.Collapsed;
            }
        }

        public void SetGiftsRect()
        {
            lifeRect = new Rect(Canvas.GetLeft(life), Canvas.GetTop(life), life.Width - 15, life.Height);
        }

    }
}
