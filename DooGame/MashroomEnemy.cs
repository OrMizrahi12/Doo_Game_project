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
        private TextBlock textBlockMashroomKill = new TextBlock();
        private ImageBrush mashroomImg, mashroomLabalImg;
        private bool mashroomCanAttak;
        private double mashroomCounterForChangeImgWalk = 0, mashroomCounterForChangeImgAttack = 0, mashroomSpeed = 7;

        public Rectangle mashroom, mashroomLabal;
        public Rect mashroomRect;
        public ProgressBar mashroomLife = new ProgressBar();
        public string mashroomAct;
        
        public MashroomEnemy()
        {
            mashroom = new Rectangle();
            mashroomImg = new ImageBrush();
            mashroomLabal = new Rectangle();
            mashroomLabalImg = new ImageBrush();
        }

        public void InitialMashroom(Canvas mainCanvas, Player player)
        {
            
            mashroom.Width = 50; mashroom.Height = 100; mashroom.Fill = mashroomImg; mashroomLabal.Fill = mashroomLabalImg;
            mashroomImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/mashroom(enemy)/mashroom_run/mashroom_run_L1.png"));
            mashroomLabalImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/mashroom(enemy)/mashroom_run/mashroom_run_L1.png"));
            mashroomLife.Value = 100; mashroomLife.Width = 50; mashroomLife.Height = 10; mashroomLife.Foreground = new SolidColorBrush(Colors.Purple);
            textBlockMashroomKill.Text = $"0/{player.mashroomKillCount}";
           
            Canvas.SetLeft(mashroom, 2000); Canvas.SetTop(mashroom, 0);
            Canvas.SetTop(mashroomLife, Canvas.GetTop(mashroom) - mashroom.Height);
            Canvas.SetLeft(mashroomLife, Canvas.GetLeft(mashroom) - (mashroom.Width / 2));

            mainCanvas.Children.Add(mashroom); mainCanvas.Children.Add(mashroomLabal);
            mainCanvas.Children.Add(mashroomLife); mainCanvas.Children.Add(textBlockMashroomKill);
        }
        public void SetMashroomRect()
        {
            mashroomRect = new Rect(Canvas.GetLeft(mashroom), Canvas.GetTop(mashroom), mashroom.Width - 15, mashroom.Height);
        }

        public void MashroomController(Player player, PhysicsDecor decor, DispatcherTimer GameTimer)
        {
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
                    player.actualMashroomKillCount += 1;
                    textBlockMashroomKill.Text = $"{player.actualMashroomKillCount}/{player.mashroomKillCount}";
                }
            }
            if (player.playerHitBox.IntersectsWith(mashroomRect) && mashroomCanAttak == true)
            {
                player.lifePlayer.Value -= 0.5;
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

        public void ResponsivMashroom(PhysicsDecor decor, EyeEnemy eyeEnemy, GoblinEnemy goblinEnemy, Player player, double W, double H)
        {
            Canvas.SetTop(mashroomLife, Canvas.GetTop(mashroom) - 10);
            Canvas.SetLeft(mashroomLife, Canvas.GetLeft(mashroom) - (mashroom.Width / 2));
            Canvas.SetTop(mashroom, Canvas.GetTop(decor.flor1) - mashroom.Height);
            Canvas.SetLeft(mashroomLabal, eyeEnemy.eyeEnemyLabal.Width + goblinEnemy.goblinLabal.Width + (mashroomLabal.Width * 2));
            Canvas.SetTop(mashroomLabal, player.lifePlayer.Height * 3);
            Canvas.SetTop(textBlockMashroomKill, mashroomLabal.Height * 2);
            Canvas.SetLeft(textBlockMashroomKill, eyeEnemy.eyeEnemyLabal.Width + goblinEnemy.goblinLabal.Width + (mashroomLabal.Width * 2));
           
            textBlockMashroomKill.FontSize = W / 50;
            mashroom.Width = W / 16; mashroom.Height = H / 14;
            mashroomLabal.Width = W / 22; mashroomLabal.Height = H / 18;
            mashroomSpeed = H / 55;
        }
    }
}
