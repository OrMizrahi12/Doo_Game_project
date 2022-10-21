using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DooGame
{
    internal class Player
    {
        private DispatcherTimer playerAttackTimerAnimation = new DispatcherTimer();
        private ImageBrush playerImg;
        private double playerAttackAnimationCounter = 1, counterForChangeAnimation = 0;
        private string diraction, lastAct;

        public Rectangle player;
        public ProgressBar jumpAbilityPlayer = new ProgressBar(); public ProgressBar lifePlayer = new ProgressBar();
        public Rect playerHitBox;
        public double playerSpeed = 10, playerForceJump = 0;
        public int goblinKillCound = 2, mashroomKillCount = 4, eyeKillCount = 3, actualGoblinKillCound = 0, actualMashroomKillCount = 0, actualEyeKillCount = 0;
        public bool playrRigth, playerLeft, playerJump, playerShooter ,prmisionToJump = true, playerAttack, playerTakeKey;
        
        public Player()
        {
            player = new Rectangle();
            playerImg = new ImageBrush();
            playerImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_walk/player_run_R1.png"));
            playerAttackTimerAnimation.Tick += playerAnimationAttackEngine;
            playerAttackTimerAnimation.Interval = TimeSpan.FromMilliseconds(20);
        }

        public void InitialPlayer(Canvas mainCanvas)
        {
            playerAttackTimerAnimation.Stop();

            actualGoblinKillCound = 0; actualMashroomKillCount = 0; actualEyeKillCount = 0;
            player.Width = 80; player.Height = 130; player.Fill = playerImg;
            jumpAbilityPlayer.Value = 100; jumpAbilityPlayer.Width = 150; jumpAbilityPlayer.Height = 20; jumpAbilityPlayer.Foreground = new SolidColorBrush(Colors.Orange);
            lifePlayer.Value = 100; lifePlayer.Width = 50; lifePlayer.Height = 10; lifePlayer.Foreground = new SolidColorBrush(Colors.Green);

            Canvas.SetTop(player, 0); Canvas.SetLeft(player, 0);        
            Canvas.SetTop(jumpAbilityPlayer, 5); Canvas.SetLeft(jumpAbilityPlayer, 5);
            Canvas.SetTop(lifePlayer, Canvas.GetTop(player) - player.Height);
            Canvas.SetLeft(lifePlayer, Canvas.GetLeft(player) - (player.Width / 2));
            
            mainCanvas.Children.Add(lifePlayer);
            mainCanvas.Children.Add(player);
            mainCanvas.Children.Add(jumpAbilityPlayer);
        }

        public void SetPlayerRects()
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
        }

        public void PlayerMoveController(double H, double W)
        {
            Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            if (playrRigth)
            {
                diraction = "Rigth"; lastAct = "move";
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
                counterForChangeAnimation += .5;
                ChangePlayerWalkAnimation(counterForChangeAnimation);
            }
            if (playerLeft)
            {
                diraction = "Left"; lastAct = "move";
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
                counterForChangeAnimation += .5;
                ChangePlayerWalkAnimation(counterForChangeAnimation);
            }
            if (playerJump && jumpAbilityPlayer.Value > 0)
            {
                lastAct = "jump";
                jumpAbilityPlayer.Value -= 5;
                prmisionToJump = false;
                Canvas.SetTop(player, Canvas.GetTop(player) - playerForceJump);
            }
            if (playerAttack)
            {
                lastAct = "attack";
                playerAttackTimerAnimation.Start();
            }
        }

        private void playerAnimationAttackEngine(object sender, EventArgs e)
        {
            ChangePlayerAttackAnimation(playerAttackAnimationCounter);
            playerAttackAnimationCounter += 1;
            if (playerAttackAnimationCounter == 11)
            {
                playerAttackAnimationCounter = 1;
                playerAttackTimerAnimation.Stop();
            }
        }
        

        public void ChangePlayerWalkAnimation(double x)
        {
            if(counterForChangeAnimation == 9) counterForChangeAnimation = 0;

            if (playrRigth == true && playerJump == false)
            {
                for (int i = 1; i <= 8; i++)
                {
                    if (i == x)
                    {
                        playerImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_walk/player_run_R{i}.png"));
                    }
                }
            }
            else if (playerLeft == true && playerJump == false)
            {
                for (int y = 1; y <= 8; y++)
                {
                    if (y == x)
                    {
                        playerImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_walk/player_run_L{y}.png"));
                    }
                }
            }
            player.Fill = playerImg;
        }

        public void ChangePlayerAttackAnimation(double x)
        {
            if (diraction == "Rigth")
            {
                for (int i = 1; i <= 11; i++)
                {
                    if (i == x)
                    {
                        playerImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_attack_v1/player_attack_R{i}.png"));
                    }
                }
            }
            else if (diraction == "Left")
            {
                for (int y = 1; y <= 11; y++)
                {
                    if (y == x)
                    {
                        playerImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_attack_v1/player_attack_L{y}.png"));
                    }
                }
            }
            player.Fill = playerImg;
        }

        public void PlayerProgressLifeEvent(DispatcherTimer GamrTimer)
        {            
            if(lifePlayer.Value > 60)
            {
                lifePlayer.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if(lifePlayer.Value < 60 && lifePlayer.Value > 30)
            {
                lifePlayer.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else if (lifePlayer.Value < 30 )
            {
                lifePlayer.Foreground = new SolidColorBrush(Colors.Red);
            }
            if (lifePlayer.Value < 1)
            {
                GamrTimer.Stop();
            }
        }

        public void ResponsivePlayer(double H, double W)
        {
            Canvas.SetTop(lifePlayer, Canvas.GetTop(player) - 15);
            Canvas.SetLeft(lifePlayer, Canvas.GetLeft(player));

            player.Width = W / 14;
            player.Height = H / 8;
            playerSpeed = H / 40;
            playerForceJump = H / 15;
        }

        public void LocatePlayerAfterLoading()
        {
            Canvas.SetTop(player, 0);
        }
    }
}
