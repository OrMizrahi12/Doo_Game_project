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
        public Rectangle player;
        public Rect playerHitBox;
        public DispatcherTimer playerAttackTimerAnimation = new DispatcherTimer();
        double counterForChangeAnimation = 0;
        public ProgressBar jumpAbilityPlayer = new ProgressBar(); public ProgressBar lifePlayer = new ProgressBar();
        private string diraction, lastAct;
        public int playerSpeed = 10;
        double playerAttackAnimationCounter = 1;
        public bool playrRigth, playerLeft, playerJump, playerShooter ,prmisionToJump = true, playerAttack;
        public ImageBrush playerImg;
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

            player.Width = 80; player.Height = 130; player.Fill = playerImg;
            Canvas.SetTop(player, 0); Canvas.SetLeft(player, 0);        
            mainCanvas.Children.Add(player);

            jumpAbilityPlayer.Value = 100; jumpAbilityPlayer.Width = 150; jumpAbilityPlayer.Height = 20; jumpAbilityPlayer.Foreground = new SolidColorBrush(Colors.Orange);
            Canvas.SetTop(jumpAbilityPlayer, 5); Canvas.SetLeft(jumpAbilityPlayer, 5);
            mainCanvas.Children.Add(jumpAbilityPlayer);

            lifePlayer.Value = 100; lifePlayer.Width = 50; lifePlayer.Height = 10; lifePlayer.Foreground = new SolidColorBrush(Colors.Green);

            Canvas.SetTop(lifePlayer, Canvas.GetTop(player) - player.Height); Canvas.SetLeft(lifePlayer, Canvas.GetLeft(player) - (player.Width / 2));
            mainCanvas.Children.Add(lifePlayer);
        }


        public void SetPlayerRects()
        {
            Canvas.SetTop(lifePlayer, Canvas.GetTop(player) - 15); Canvas.SetLeft(lifePlayer, Canvas.GetLeft(player) );
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
        }

        public void PlayerMoveController(Canvas mainCanvas, Player playerCopy)
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
                Canvas.SetTop(player, Canvas.GetTop(player) - 30);
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
    }
}
