using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    internal class PhaseToolsLevel
    {
        private Random random = new Random();
        private TextBlock winMsg;
        private ImageBrush doorImg, KeyImg, playerWinImg;
        private int keyDistance = 4500, doorDistance = 6500;

        public bool winTheGame;
        public Rectangle door, key, playerWin;
        public Rect doorRect, keyRect;
        public int level = 1;
           
        public PhaseToolsLevel()
        {
            winMsg = new TextBlock();
            door = new Rectangle();
            key = new Rectangle();
            playerWin = new Rectangle();
            doorImg = new ImageBrush();
            KeyImg = new ImageBrush();
            playerWinImg = new ImageBrush();    
        }

        public void InitialPhaseTools(Canvas mainCanvas, PhysicsDecor decor)
        {
            doorImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/for_finish_level/door_locked_.png"));
            KeyImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/for_finish_level/key.png"));
            playerWinImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/player/player_attack_v1/player_attack_R11.png"));
            winMsg.Foreground = new SolidColorBrush(Colors.Red);

            playerWin.Height = 200; playerWin.Width = 100; playerWin.Fill = playerWinImg; 
            key.Width = 15; key.Height = 40; key.Fill = KeyImg;
            door.Width = 120; door.Height = 200; door.Fill = doorImg;

            key.Visibility = Visibility.Visible;
            winMsg.Visibility = Visibility.Collapsed; 
            playerWin.Visibility = Visibility.Collapsed;

            Canvas.SetTop(door, Canvas.GetTop(decor.flor1)); Canvas.SetTop(key, Canvas.GetTop(decor.flor1));
            Canvas.SetLeft(door, doorDistance); Canvas.SetLeft(key, keyDistance);
            Canvas.SetLeft(playerWin, 200); Canvas.SetTop(playerWin, 200);

            mainCanvas.Children.Add(door);
            mainCanvas.Children.Add(key);
            mainCanvas.Children.Add(winMsg);
            mainCanvas.Children.Add(playerWin);
        }

        public void SetPhaseToolsRect()
        {
            doorRect = new Rect(Canvas.GetLeft(door), Canvas.GetTop(door), door.Width, door.Height);
            keyRect = new Rect(Canvas.GetLeft(key), Canvas.GetTop(key), key.Width, key.Height);
        }

        public void PlayerTakeKey(Player player)
        {
            if (player.playerHitBox.IntersectsWith(keyRect))
            {
                key.Visibility = Visibility.Collapsed;
                player.playerTakeKey = true;
            }
        }


        public void PlayerOpenDoor(Player player, DispatcherTimer GameTimer, Canvas mainCanvas ,double W, double H)
        {
            if (player.playerHitBox.IntersectsWith(doorRect) && player.playerTakeKey)
            {
                if(player.actualEyeKillCount >= player.eyeKillCount && player.actualGoblinKillCound >= player.goblinKillCound && player.actualMashroomKillCount >= player.mashroomKillCount)
                {
                    GameTimer.Stop();
                    mainCanvas.Children.Clear();
                    mainCanvas.Background = new SolidColorBrush(Colors.Black);

                    winMsg.Visibility = Visibility.Visible;
                    playerWin.Visibility = Visibility.Visible;
                    playerWin.Visibility = Visibility.Visible;
                    key.Visibility = Visibility.Visible;

                    player.playerTakeKey = false;
                    level += 1;

                    player.goblinKillCound += random.Next(3, 5);
                    player.mashroomKillCount += random.Next(3, 5);
                    player.eyeKillCount += random.Next(3, 5);

                    keyDistance += 2000; doorDistance += 2000;
                    winMsg.Text = $"WIN! to level {level} press ENTER -->";

                    mainCanvas.Children.Add(winMsg);
                    mainCanvas.Children.Add(playerWin);

                    doorImg.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/for_finish_level/door_unloked.png"));
                    door.Fill = doorImg;
                }
                
            }
             
        }

        public void ResponsivePhaseTools(PhysicsDecor decor, double H, double W)
        {
            Canvas.SetTop(door, Canvas.GetTop(decor.flor1) - door.Height);
            Canvas.SetTop(winMsg, H / 2 + playerWin.Height);
            Canvas.SetLeft(winMsg, W / 2 - winMsg.Width / 2);
            Canvas.SetTop(key, Canvas.GetTop(decor.flor1) - key.Height);
            Canvas.SetLeft(playerWin, (W / 2) - playerWin.Width);
            Canvas.SetTop(playerWin, (H / 2) - playerWin.Height);

            if (doorRect.IntersectsWith(decor.blobkDownRect))
            {
                Canvas.SetLeft(door, Canvas.GetLeft(door) + W / 5);
            }

            playerWin.Width = W / 5; playerWin.Height = H / 5;
            winMsg.FontSize = W / 30;
            door.Width = W / 10; door.Height = H / 4;
            key.Width = W / 16; key.Height = H / 14;
        }

        public void LocatePhaseToolsAfterLoading(PhysicsDecor decor)
        {
            Canvas.SetTop(key, Canvas.GetTop(decor.ground3));
        }
    }
}
