using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Threading;

namespace DooGame
{
    internal class EyeEnemy 
    {
        private ImageBrush eyeEnemyImg, eyeEnemyLabalImg;
        private bool eyeEnemyCanAttak;
        private TextBlock textBlockEyeKill = new TextBlock();

        public Rectangle eyeEnemy, eyeEnemyLabal;
        public Rect eyeEnemyRect;
        public ProgressBar eyeEnemyLife = new ProgressBar();
        public string eyeEnemyAct;
        public double eyeEnemyCounterForChangeImgWalk = 0, eyeEnemyCounterForChangeImgAttack = 0, eyeEnemySpeed = 3;
       
        public EyeEnemy()
        {
            eyeEnemy = new Rectangle();
            eyeEnemyImg = new ImageBrush();
            eyeEnemyLabal = new Rectangle();
            eyeEnemyLabalImg = new ImageBrush();
        }

        public void InitialEyeEnemy(Canvas mainCanvas, Player player)
        {
            textBlockEyeKill.Text = $"0/{player.eyeKillCount}";
            eyeEnemyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_flight_R1.png"));
            eyeEnemyLabalImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_flight_R1.png"));

            eyeEnemy.Width = 50; eyeEnemy.Height = 100; eyeEnemy.Fill = eyeEnemyImg; eyeEnemyLabal.Fill = eyeEnemyLabalImg;
            eyeEnemyLife.Value = 100; eyeEnemyLife.Width = 50; eyeEnemyLife.Height = 10; 
            eyeEnemyLife.Foreground = new SolidColorBrush(Colors.Purple);
           
            Canvas.SetLeft(eyeEnemy, 2000); Canvas.SetTop(eyeEnemy, 40);
            Canvas.SetTop(eyeEnemyLife, Canvas.GetTop(eyeEnemy) - eyeEnemy.Height); 
            Canvas.SetLeft(eyeEnemyLife, Canvas.GetLeft(eyeEnemy) - (eyeEnemy.Width / 2)); 
           
            mainCanvas.Children.Add(eyeEnemy); mainCanvas.Children.Add(eyeEnemyLife); mainCanvas.Children.Add(eyeEnemyLabal);
            mainCanvas.Children.Add(textBlockEyeKill);
        }

        public void SetEyeEnemyRect()
        {
            eyeEnemyRect = new Rect(Canvas.GetLeft(eyeEnemy), Canvas.GetTop(eyeEnemy), eyeEnemy.Width - 15, eyeEnemy.Height);
        }

        public void EyeEnemyController(Player player, PhysicsDecor decor, DispatcherTimer GameTimer)
        {            
            if (Canvas.GetLeft(eyeEnemy) < Canvas.GetLeft(player.player) && !eyeEnemyCanAttak)
            {
                Canvas.SetLeft(eyeEnemy, Canvas.GetLeft(eyeEnemy) + eyeEnemySpeed);
                eyeEnemyAct = "Rigth";
            }
            else if (Canvas.GetLeft(eyeEnemy) > Canvas.GetLeft(player.player) && !eyeEnemyCanAttak)
            {
                eyeEnemyAct = "Left";
                Canvas.SetLeft(eyeEnemy, Canvas.GetLeft(eyeEnemy) - eyeEnemySpeed);
            }

            if (Canvas.GetTop(player.player) < Canvas.GetTop(eyeEnemy))
            {
                Canvas.SetTop(eyeEnemy, Canvas.GetTop(eyeEnemy) - 1);
            }
            else if (Canvas.GetTop(player.player) > Canvas.GetTop(eyeEnemy))
            {
                Canvas.SetTop(eyeEnemy, Canvas.GetTop(eyeEnemy) + 1);
            }

            if (eyeEnemyRect.IntersectsWith(player.playerHitBox))
            {
                eyeEnemyCanAttak = true;
            }
            else 
            {
                eyeEnemyCanAttak = false;
            } 

            if (!eyeEnemyCanAttak)
            {
                if (eyeEnemyCounterForChangeImgWalk > 8)
                {
                    eyeEnemyCounterForChangeImgWalk = 1;
                } 
                eyeEnemyCounterForChangeImgWalk += 0.5;
                ChangeEyeEnemyWalkAnimation(eyeEnemyCounterForChangeImgWalk);
            }
            else if (eyeEnemyCanAttak)
            {
                if (eyeEnemyCounterForChangeImgAttack > 8)
                {
                    eyeEnemyCounterForChangeImgAttack = 1;
                } 
                eyeEnemyCounterForChangeImgAttack += 0.5;
                ChangeEyeEnemyAttackAnimation(eyeEnemyCounterForChangeImgAttack);
            }

            if (player.playerHitBox.IntersectsWith(eyeEnemyRect) && player.playerAttack == true)
            {
                eyeEnemyLife.Value -= 10;

                if (eyeEnemyLife.Value == 0)
                {
                    Canvas.SetLeft(eyeEnemy, 1500);
                    eyeEnemyLife.Value = 100;
                    player.actualEyeKillCount += 1;
                    textBlockEyeKill.Text = $"{player.actualEyeKillCount}/{player.eyeKillCount}";
                }
            }
            if (player.playerHitBox.IntersectsWith(eyeEnemyRect) && eyeEnemyCanAttak == true)
            {
                player.lifePlayer.Value -= 1;
            }
            if(Canvas.GetLeft(eyeEnemy) < -50)
            {
                Canvas.SetLeft(eyeEnemy, 1500);
            }
        }

        public void ChangeEyeEnemyWalkAnimation(double x)
        {
            if (eyeEnemyAct == "Left")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        eyeEnemyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_flight_R{i}.png"));
                    }
                }
            }
            else if (eyeEnemyAct == "Rigth")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        eyeEnemyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_flight_R{i}.png"));
                    }
                }
            }
            eyeEnemy.Fill = eyeEnemyImg;
        }

        public void ChangeEyeEnemyAttackAnimation(double x)
        {
            if (eyeEnemyAct == "Left")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        eyeEnemyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_attack_L{i}.png"));
                    }
                }
            }
            else if (eyeEnemyAct == "Rigth")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        eyeEnemyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/eye(enemy)/eye_attack_R{i}.png"));
                    }
                }
            }
            eyeEnemy.Fill = eyeEnemyImg;
        }

        public void ResponsiveEyeEnemy(Player player, double H, double W)
        {
            Canvas.SetLeft(eyeEnemyLabal, 5);
            Canvas.SetTop(eyeEnemyLabal, player.lifePlayer.Height * 3);
            Canvas.SetTop(textBlockEyeKill, eyeEnemyLabal.Height * 2);
            Canvas.SetLeft(textBlockEyeKill, 5);
            Canvas.SetTop(eyeEnemyLife, Canvas.GetTop(eyeEnemy) - 10); 
            Canvas.SetLeft(eyeEnemyLife, Canvas.GetLeft(eyeEnemy) - (eyeEnemy.Width / 2));

            textBlockEyeKill.FontSize = W / 50;
            eyeEnemy.Width = W / 16; eyeEnemy.Height = H / 8;
            eyeEnemyLabal.Width = W / 22; eyeEnemyLabal.Height = H / 18;
            eyeEnemySpeed = H / 80;
        }
    }
}
