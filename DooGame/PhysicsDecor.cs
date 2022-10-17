using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
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
    internal class PhysicsDecor
    {
        public Rectangle ground1, ground2, ground3 ,background1, background2, blockUp, blockDown, flor1,flor2;
        public Rect groundRect1, groundRect2, groundRect3, blockUpRect, blobkDownRect, flor1Rect, flor2Rect;
        Random random = new Random();
        bool blocksCanBeShut = true;
        int movingGroundSpeed = 2, groundMovingCounter = 0;
        ImageBrush background ,ground, blobkUpImg, blockDownImg, florImg;
        public PhysicsDecor()
        {
            flor1 = new Rectangle(); flor2 = new Rectangle();
            ground1 = new Rectangle();ground2 = new Rectangle(); ground3 = new Rectangle();
            blockDown = new Rectangle(); blockUp = new Rectangle();            
            background1 = new Rectangle(); background2 = new Rectangle(); 
            background = new ImageBrush(); ground = new ImageBrush(); blobkUpImg = new ImageBrush();
            blockDownImg = new ImageBrush(); florImg = new ImageBrush();
        }

        public void InitialDecor(Canvas mainCanvas)
        {
            movingGroundSpeed = 2; groundMovingCounter = 0; 

            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/figmQY.gif"));
            ground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/gA.png"));
            blobkUpImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/blockFromUpImg.png"));
            blockDownImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/blockFromDownImg.png"));
            florImg.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/flor.png"));

            background1.Width = 1600; background2.Width = 1600; background1.Height = 900; background2.Height = 900;
            ground1.Width = 250; ground2.Width = 250; ground3.Width = 250; ground1.Height = 100; ground2.Height = 100; ground3.Height = 100;
            blockUp.Width = 80; blockUp.Height = 400; blockDown.Width = 80; blockDown.Height = 400;
            flor1.Height = 250; flor1.Width = 1600; flor2.Height = 250; flor2.Width = 1600;

            flor1.Fill = florImg; flor2.Fill = florImg;
            background1.Fill = background; background2.Fill = background;           
            ground1.Fill = ground; ground2.Fill = ground; ground3.Fill = ground;
            blockUp.Fill = blobkUpImg; blockDown.Fill = blockDownImg;   
           
            Canvas.SetTop(ground1, 400); Canvas.SetLeft(ground1, 0);
            Canvas.SetTop(ground2, 600); Canvas.SetLeft(ground2, 600);
            Canvas.SetTop(ground3, 250); Canvas.SetLeft(ground3, 1100);
            Canvas.SetLeft(background1, 0); Canvas.SetLeft(background2, 1600);
            Canvas.SetTop(blockUp, -200); Canvas.SetLeft(blockUp, 2000);
            Canvas.SetTop(blockDown, 600); Canvas.SetLeft(blockDown, 2000);
            Canvas.SetTop(flor1, 700); Canvas.SetLeft(flor1, 0);
            Canvas.SetTop(flor2, 700); Canvas.SetLeft(flor2, 1600);

            mainCanvas.Children.Add(background1); mainCanvas.Children.Add(background2);
            mainCanvas.Children.Add(ground1); mainCanvas.Children.Add(ground2); mainCanvas.Children.Add(ground3);
            mainCanvas.Children.Add(blockUp); mainCanvas.Children.Add(blockDown);
            mainCanvas.Children.Add(flor1); mainCanvas.Children.Add(flor2);
        }

        public void SetGroundRects()
        {
            groundRect1 = new Rect(Canvas.GetLeft(ground1), Canvas.GetTop(ground1), ground1.Width - 15, ground1.Height);
            groundRect2 = new Rect(Canvas.GetLeft(ground2), Canvas.GetTop(ground2), ground2.Width - 15, ground2.Height);
            groundRect3 = new Rect(Canvas.GetLeft(ground3), Canvas.GetTop(ground3), ground3.Width - 15, ground3.Height);
            blockUpRect = new Rect(Canvas.GetLeft(blockUp), Canvas.GetTop(blockUp), blockUp.Width - 15, blockUp.Height);
            blobkDownRect = new Rect(Canvas.GetLeft(blockDown), Canvas.GetTop(blockDown), blockDown.Width - 15, blockDown.Height);
            flor1Rect = new Rect(Canvas.GetLeft(flor1), Canvas.GetTop(flor1), flor1.Width - 15, flor1.Height);
            flor2Rect = new Rect(Canvas.GetLeft(flor2), Canvas.GetTop(flor2), flor2.Width - 15, flor2.Height);
        }

        public void GroundPhysics(Player player, DispatcherTimer GameTimer, Canvas mainCanvas)
        {
            if  (
                 groundRect1.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground1) - player.player.Height) ||
                 groundRect2.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground2) - player.player.Height) ||
                 groundRect3.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) == (Canvas.GetTop(ground3) - player.player.Height)
                )
            {
                Canvas.SetTop(player.player, Canvas.GetTop(player.player) - player.playerSpeed);
                if (player.jumpAbilityPlayer.Value < 100) player.jumpAbilityPlayer.Value += 5;
            }
            if (
                groundRect1.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= Canvas.GetTop(ground1) - ground1.Height && Canvas.GetLeft(player.player) < Canvas.GetLeft(ground1) ||
                groundRect2.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= Canvas.GetTop(ground2) - ground2.Height && Canvas.GetLeft(player.player) < Canvas.GetLeft(ground2) ||
                groundRect3.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= Canvas.GetTop(ground3) - ground3.Height && Canvas.GetLeft(player.player) < Canvas.GetLeft(ground3)
               )
            {
                player.playrRigth = false;
            } 
            if (
                 groundRect1.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= (Canvas.GetTop(ground1) - ground1.Height) && Canvas.GetLeft(player.player) > Canvas.GetLeft(ground1) ||
                 groundRect2.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= (Canvas.GetTop(ground2) - ground2.Height) && Canvas.GetLeft(player.player) > Canvas.GetLeft(ground2) ||
                 groundRect3.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) >= (Canvas.GetTop(ground3) - ground3.Height) && Canvas.GetLeft(player.player) > Canvas.GetLeft(ground3)
               )
            {
                player.playerLeft = false;
            } 
            if (
                 groundRect1.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) > Canvas.GetTop(ground1) ||
                 groundRect2.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) > Canvas.GetTop(ground2) ||
                 groundRect3.IntersectsWith(player.playerHitBox) && Canvas.GetTop(player.player) > Canvas.GetTop(ground3)
               )
            {
                player.playerJump = false;
            } 
            if (Canvas.GetTop(player.player) <= 0)
            {
                player.playerJump = false;
            } 
            if ((player.playerHitBox.IntersectsWith(flor1Rect) || player.playerHitBox.IntersectsWith(flor2Rect)) && Canvas.GetTop(player.player) == 600)
            {
                Canvas.SetTop(player.player, Canvas.GetTop(player.player) - player.playerSpeed);
                player.jumpAbilityPlayer.Value += 5;
            }
        }

        public void MoveScreen(Player player, GoblinEnemy goblinEnemy, EyeEnemy eyeEnemy, GiftPlayer giftPlayer)
        {
            if (Canvas.GetLeft(player.player) > 500 && player.playrRigth == true)
            {
                Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 10); Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 10);
                Canvas.SetLeft(ground1, Canvas.GetLeft(ground1) - 10); Canvas.SetLeft(ground2, Canvas.GetLeft(ground2) - 10); Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) - 10);
                Canvas.SetLeft(flor1, Canvas.GetLeft(flor1) - 10); Canvas.SetLeft(flor2, Canvas.GetLeft(flor2) - 10);
                Canvas.SetLeft(blockUp, Canvas.GetLeft(blockUp) - 10); Canvas.SetLeft(blockDown, Canvas.GetLeft(blockDown) - 10);
                Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) - 10);
                Canvas.SetLeft(goblinEnemy.goblin, Canvas.GetLeft(goblinEnemy.goblin) - 10);
                Canvas.SetLeft(eyeEnemy.eyeEnemy, Canvas.GetLeft(eyeEnemy.eyeEnemy) - 10);
                Canvas.SetLeft(giftPlayer.life, Canvas.GetLeft(giftPlayer.life) - 10);
            }
            else if(Canvas.GetLeft(player.player) < 500 && player.playerLeft == true)
            {
                Canvas.SetLeft(background1, Canvas.GetLeft(background1) + 10);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) + 10);
                Canvas.SetLeft(ground1, Canvas.GetLeft(ground1) + 10);
                Canvas.SetLeft(ground2, Canvas.GetLeft(ground2) + 10);
                Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) + 10);
                Canvas.SetLeft(flor1, Canvas.GetLeft(flor1) + 10);
                Canvas.SetLeft(flor2, Canvas.GetLeft(flor2) + 10);
                Canvas.SetLeft(blockUp, Canvas.GetLeft(blockUp) + 10);
                Canvas.SetLeft(blockDown, Canvas.GetLeft(blockDown) + 10);
                Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) + 10);
                Canvas.SetLeft(goblinEnemy.goblin, Canvas.GetLeft(goblinEnemy.goblin) + 10);
                Canvas.SetLeft(eyeEnemy.eyeEnemy, Canvas.GetLeft(eyeEnemy.eyeEnemy) + 10);
                Canvas.SetLeft(giftPlayer.life, Canvas.GetLeft(giftPlayer.life) + 10);
            }
        }

        public void GroundLocator(Player player)
        {
            if (player.playrRigth)
            {
                if (Canvas.GetLeft(ground1) < -300)
                {
                    Canvas.SetLeft(ground1, 1600);
                }   
                else if (Canvas.GetLeft(ground2) < -300)
                {
                    Canvas.SetLeft(ground2, 1600);
                }   
                else if (Canvas.GetLeft(ground3) < -300)
                {
                    Canvas.SetLeft(ground3, 1600);
                }
            }
        }

        public void MoveDecor(Player player, DispatcherTimer GameTimer)
        {
            BlockMoving(player, GameTimer);
            GroundMoving(player, GameTimer); GameEnforcement(player, GameTimer);
        }

        private void GroundMoving(Player player , DispatcherTimer GameTimer)
        {
            if (groundMovingCounter < 30)
            {
                Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) - movingGroundSpeed);
                if (player.playerHitBox.IntersectsWith(groundRect3)) Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) - movingGroundSpeed);
            }
            else if (groundMovingCounter > 30)
            {
                Canvas.SetLeft(ground3, Canvas.GetLeft(ground3) + movingGroundSpeed);
                if (groundMovingCounter > 58)
                {
                    groundMovingCounter = 0;
                }
                if (player.playerHitBox.IntersectsWith(groundRect3))
                {
                    Canvas.SetLeft(player.player, Canvas.GetLeft(player.player) + movingGroundSpeed);
                }
            }
            groundMovingCounter++;
        }

        private void BlockMoving(Player player, DispatcherTimer GameTimer)
        {
            if (blockUpRect.IntersectsWith(blobkDownRect) == false && blocksCanBeShut == true)
            {
                Canvas.SetTop(blockUp, Canvas.GetTop(blockUp) + 10); Canvas.SetTop(blockDown, Canvas.GetTop(blockDown) - 10);
            }
            else if (blockUpRect.IntersectsWith(blobkDownRect))
            {
                blocksCanBeShut = false;
            }
            if (blocksCanBeShut == false)
            {
                Canvas.SetTop(blockUp, Canvas.GetTop(blockUp) - 10); Canvas.SetTop(blockDown, Canvas.GetTop(blockDown) + 10);

                if (Canvas.GetTop(blockUp) < -300 || Canvas.GetTop(blockUp) > 700)
                {
                    blocksCanBeShut = true;
                } 
            }
            if (Canvas.GetLeft(blockUp) < -100)
            {
                int randomBlocksLocation = random.Next(1000, 1400);
                Canvas.SetLeft(blockUp, randomBlocksLocation); Canvas.SetTop(blockUp, -300);
                Canvas.SetLeft(blockDown, randomBlocksLocation); Canvas.SetTop(blockDown, 700);
            }
        }

        private void GameEnforcement(Player player, DispatcherTimer GameTimer)
        {
            if (player.playerHitBox.IntersectsWith(blockUpRect) || player.playerHitBox.IntersectsWith(blobkDownRect))
            {
                GameTimer.Stop();
            }
        }

        public void MakeBackGroundInfinity()
        {
            if (Canvas.GetLeft(background1) < -background1.Width) Canvas.SetLeft(background1, background2.Width - 15);
            if (Canvas.GetLeft(background2) < -background2.Width) Canvas.SetLeft(background2, background1.Width - 15);
            if (Canvas.GetLeft(background2) > background2.Width) Canvas.SetLeft(background2, -background1.Width + 15);
            if (Canvas.GetLeft(background1) > background1.Width) Canvas.SetLeft(background1, -background1.Width + 15);

            if (Canvas.GetLeft(flor1) < -flor1.Width) Canvas.SetLeft(flor1, flor2.Width - 15);
            if (Canvas.GetLeft(flor2) < -flor2.Width) Canvas.SetLeft(flor2, flor1.Width - 15);
            if (Canvas.GetLeft(flor2) > flor2.Width) Canvas.SetLeft(flor2, -flor1.Width + 15);
            if (Canvas.GetLeft(flor1) > flor1.Width) Canvas.SetLeft(flor1, -flor1.Width + 15);
        }
    }
}

