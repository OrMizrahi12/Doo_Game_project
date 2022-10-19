using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows;

namespace DooGame
{
    internal class MashroomEnemy
    {
        public Rectangle mashroom;
        public Rect mashroomRect;
        public ImageBrush mashroomImg;
        public ProgressBar mashroomLife = new ProgressBar();
        public string mashroomAct;
        bool mashroomCanAttak;
        double mashroomCounterForChangeImgWalk = 0, mashroomCounterForChangeImgAttack = 0;
        int mashroomSpeed = 7;
        public MashroomEnemy()
        {
            mashroom = new Rectangle();
            mashroomImg = new ImageBrush();
        }

        public void InitialMashroom(Canvas mainCanvas)
        {
            mashroom.Width = 50; mashroom.Height = 100; mashroom.Fill = mashroomImg;
            mashroomImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/mashroom(enemy)/mashroom_run/mashroom_run_L1.png"));
            mashroomLife.Value = 100; mashroomLife.Width = 50; mashroomLife.Height = 10; mashroomLife.Foreground = new SolidColorBrush(Colors.Purple);

            Canvas.SetLeft(mashroom, 2000); Canvas.SetTop(mashroom, 0);
            Canvas.SetTop(mashroomLife, Canvas.GetTop(mashroom) - mashroom.Height); Canvas.SetLeft(mashroomLife, Canvas.GetLeft(mashroom) - (mashroom.Width / 2));

            mainCanvas.Children.Add(mashroom);
            mainCanvas.Children.Add(mashroomLife);
        }
        public void SetMashroomRect()
        {
            mashroomRect = new Rect(Canvas.GetLeft(mashroom), Canvas.GetTop(mashroom), mashroom.Width - 15, mashroom.Height);
        }

        public void MashroomController(Player player, PhysicsDecor decor, DispatcherTimer GameTimer)
        {
            Canvas.SetTop(mashroomLife, Canvas.GetTop(mashroom) - 10); Canvas.SetLeft(mashroomLife, Canvas.GetLeft(mashroom) - (mashroom.Width / 2));


            Canvas.SetTop(mashroom, Canvas.GetTop(mashroom) + mashroomSpeed);
            if (mashroomRect.IntersectsWith(decor.flor1Rect) || mashroomRect.IntersectsWith(decor.flor2Rect))
            {
                Canvas.SetTop(mashroom, Canvas.GetTop(mashroom) - mashroomSpeed);
               
            }

            if (Canvas.GetLeft(mashroom) < Canvas.GetLeft(player.player) && !mashroomCanAttak)
            {
                Canvas.SetLeft(mashroom, Canvas.GetLeft(mashroom) + mashroomSpeed);
                mashroomAct = "Rigth";
            }
            else if (Canvas.GetLeft(mashroom) > Canvas.GetLeft(player.player) && !mashroomCanAttak)
            {
                mashroomAct = "Left";
                Canvas.SetLeft(mashroom, Canvas.GetLeft(mashroom) - mashroomSpeed);
            }

            if (mashroomRect.IntersectsWith(player.playerHitBox))
            {
                mashroomCanAttak = true;
            }
            else
            {
                mashroomCanAttak = false;
            }

            if (!mashroomCanAttak)
            {
                if (mashroomCounterForChangeImgWalk > 8)
                {
                    mashroomCounterForChangeImgWalk = 1;
                }
                mashroomCounterForChangeImgWalk += 0.5;
                ChangeMashroomWalkAnimation(mashroomCounterForChangeImgWalk);
            }
            else if (mashroomCanAttak)
            {
                if (mashroomCounterForChangeImgAttack > 8)
                {
                    mashroomCounterForChangeImgAttack = 1;
                }
                mashroomCounterForChangeImgAttack += 0.5;
                ChangeMashroomAttackAnimation(mashroomCounterForChangeImgAttack);
            }

            if (player.playerHitBox.IntersectsWith(mashroomRect) && player.playerAttack == true)
            {
                mashroomLife.Value -= 10;

                if (mashroomLife.Value == 0)
                {
                    Canvas.SetLeft(mashroom, 1500);
                    mashroomLife.Value = 100;
                }
            }
            if (player.playerHitBox.IntersectsWith(mashroomRect) && mashroomCanAttak == true)
            {
                player.lifePlayer.Value -= 0.5;
                if (player.lifePlayer.Value == 0)
                {
                    GameTimer.Stop();
                }
            }
            if (Canvas.GetLeft(mashroom) < -50)
            {
                Canvas.SetLeft(mashroom, 1500);
            }
        }

        public void ChangeMashroomWalkAnimation(double x)
        {
            if (mashroomAct == "Left")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        mashroomImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/mashroom(enemy)/mashroom_run/mashroom_run_L{i}.png"));
                    }
                }
            }
            else if (mashroomAct == "Rigth")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        mashroomImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/mashroom(enemy)/mashroom_run/mashroom_run_R{i}.png"));
                    }
                }
            }
            mashroom.Fill = mashroomImg;
        }

        public void ChangeMashroomAttackAnimation(double x)
        {
            if (mashroomAct == "Left")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        mashroomImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/mashroom(enemy)/mashroom_attack/mashroom_attack_L{i}.png"));
                    }
                }
            }
            else if (mashroomAct == "Rigth")
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (x == i)
                    {
                        mashroomImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/mashroom(enemy)/mashroom_attack/mashroom_attack_R{i}.png"));
                    }
                }
            }
            mashroom.Fill = mashroomImg;
        }
    }
}
