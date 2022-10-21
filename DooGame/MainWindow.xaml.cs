using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DooGame
{
    public partial class MainWindow : Window
    {
        PhysicsDecor decor = new PhysicsDecor();
        Player player = new Player();
        DispatcherTimer GameTimer = new DispatcherTimer();
        GoblinEnemy goblinEnemy = new GoblinEnemy();
        EyeEnemy eyeEnemy = new EyeEnemy();
        GiftPlayer giftPlayer = new GiftPlayer();
        MashroomEnemy mashroomEnemy = new MashroomEnemy();
        KeyAndDoor keyAndDoor = new KeyAndDoor();

        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
            ResetGame();
            mainCanvas.Focus();
        }

        private void InitialGame()
        { 
            mainCanvas.Focus();
            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
        }

        public interface ISupportState<T>
        {
            void RestoreState(T state);
            T SaveState();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            Responsive();
            player.PlayerProgressLifeEvent(GameTimer);
            decor.SetGroundRects();
            player.SetPlayerRects();
            giftPlayer.SetGiftsRect();
            goblinEnemy.SetGoblinRect();
            eyeEnemy.SetEyeEnemyRect();
            mashroomEnemy.SetMashroomRect();
            keyAndDoor.SetDoorAndKeyRect();
            decor.GroundPhysics(player, this.ActualHeight);
            player.PlayerMoveController(this.ActualHeight, this.ActualWidth);
            decor.MoveScreen(player, goblinEnemy, eyeEnemy, giftPlayer, mashroomEnemy,keyAndDoor, this.ActualHeight);
            decor.MakeBackGroundInfinity(this.ActualHeight, this.ActualWidth);
            decor.GroundLocator(player, this.ActualWidth);
            decor.MoveDecor(player, GameTimer, this.ActualHeight);
            goblinEnemy.GoblinController(player, decor, GameTimer);
            eyeEnemy.EyeEnemyController(player, decor, GameTimer);
            mashroomEnemy.MashroomController(player, decor, GameTimer); 
            giftPlayer.PlayerGiftTouchEvent(player, decor);
            keyAndDoor.PlayerTakeKey(player);
            keyAndDoor.PlayerOpenDoor(player,GameTimer, mainCanvas, this.ActualWidth, this.ActualHeight);
        }
        
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right) 
            {
                player.playrRigth = true;
            } 
            else if (e.Key == Key.Left)
            {
                player.playerLeft = true;
            }
            else if (e.Key == Key.Space) 
            {
                player.playerJump = true;
            }
            else if (e.Key == Key.V)
            {
                player.playerShooter = true;
            }
            else if (e.Key == Key.D)
            {
                player.playerAttack = true;
            }
            else if (e.Key == Key.Enter )
            {
                ResetGame();
            } 
            else if (e.Key == Key.P)
            {
              GameTimer.Stop();
            }
            else if (e.Key == Key.O)
            {
                GameTimer.Start();
            }
        }
        
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Right)
            {
                player.playrRigth = false;
            }
            else if (e.Key == Key.Left)
            {
                player.playerLeft = false;
            }
            else if (e.Key == Key.Space)
            {
                player.playerJump = false;
            }
            else if (e.Key == Key.D)
            {
                player.playerAttack = false;
            }  
        }

        private void ResetGame()
        {
            mainCanvas.Children.Clear();
            decor.InitialDecor(mainCanvas, this.ActualHeight, this.ActualWidth, keyAndDoor);
            player.InitialPlayer(mainCanvas);
            goblinEnemy.InitialGoblin(mainCanvas,player);
            eyeEnemy.InitialEyeEnemy(mainCanvas , player);
            mashroomEnemy.InitialMashroom(mainCanvas, player);
            giftPlayer.InitialGifts(mainCanvas, decor);
            keyAndDoor.InitialDoorAndKey(mainCanvas, decor);
            GameTimer.Start();
            keyAndDoor.playerWin.Visibility = Visibility.Hidden;
        }

        private void Responsive()
        {
            double H = this.ActualHeight; double W = this.ActualWidth;

            Canvas.SetTop(decor.ground1, H / 3); Canvas.SetTop(decor.ground2, H / 2); Canvas.SetTop(decor.ground3, H / 3.5);
            Canvas.SetTop(decor.flor1, H - decor.flor1.Height);Canvas.SetTop(decor.flor2, H - decor.flor1.Height) ;
            Canvas.SetTop(keyAndDoor.door, Canvas.GetTop(decor.flor1) -keyAndDoor.door.Height);
            Canvas.SetTop(keyAndDoor.winMsg, H/2 + keyAndDoor.playerWin.Height);
            Canvas.SetLeft(keyAndDoor.winMsg, W/ 2 - keyAndDoor.winMsg.Width/2);
            Canvas.SetTop(keyAndDoor.key, Canvas.GetTop(decor.flor1) -keyAndDoor.key.Height);
            Canvas.SetTop(goblinEnemy.goblin, Canvas.GetTop(decor.flor1) - goblinEnemy.goblin.Height);
            Canvas.SetTop(mashroomEnemy.mashroom, Canvas.GetTop(decor.flor1) - mashroomEnemy.mashroom.Height);
            Canvas.SetLeft(keyAndDoor.playerWin, (W / 2) - keyAndDoor.playerWin.Width); Canvas.SetTop(keyAndDoor.playerWin, (H / 2) - keyAndDoor.playerWin.Height);
           
            Canvas.SetLeft(eyeEnemy.eyeEnemyLabal, 5); 
            Canvas.SetLeft(goblinEnemy.goblinLabal,  eyeEnemy.eyeEnemyLabal.Width + (goblinEnemy.goblinLabal.Width * 1));
            Canvas.SetLeft(mashroomEnemy.mashroomLabal,  eyeEnemy.eyeEnemyLabal.Width + goblinEnemy.goblinLabal.Width + (mashroomEnemy.mashroomLabal.Width * 2));
          
            Canvas.SetTop(goblinEnemy.goblinLabal, player.lifePlayer.Height * 3);
            Canvas.SetTop(eyeEnemy.eyeEnemyLabal, player.lifePlayer.Height * 3);
            Canvas.SetTop(mashroomEnemy.mashroomLabal, player.lifePlayer.Height * 3);

            Canvas.SetTop(goblinEnemy.textBlockGoblinKill, goblinEnemy.goblinLabal.Height * 2 ); 
            Canvas.SetTop(eyeEnemy.textBlockEyeKill, eyeEnemy.eyeEnemyLabal.Height * 2); 
            Canvas.SetTop(mashroomEnemy.textBlockMashroomKill, mashroomEnemy.mashroomLabal.Height * 2);

            Canvas.SetLeft(eyeEnemy.textBlockEyeKill, 5);
            Canvas.SetLeft(goblinEnemy.textBlockGoblinKill, eyeEnemy.eyeEnemyLabal.Width + goblinEnemy.goblinLabal.Width + mashroomEnemy.mashroomLabal.Width /2 );
            Canvas.SetLeft(mashroomEnemy.textBlockMashroomKill, eyeEnemy.eyeEnemyLabal.Width + goblinEnemy.goblinLabal.Width + (mashroomEnemy.mashroomLabal.Width * 2));


            mashroomEnemy.textBlockMashroomKill.FontSize = W / 50;
            goblinEnemy.textBlockGoblinKill.FontSize = W / 50;
            eyeEnemy.textBlockEyeKill.FontSize = W / 50;

            decor.background1.Height = H; decor.background2.Height = H;
            decor.ground1.Width = W / 8; decor.ground2.Width = W / 8; decor.ground3.Width = W / 8;
            decor.blockDown.Height = H / 2;decor.blockUp.Height = H /2;
            player.player.Width = W / 14;player.player.Height = H / 8;
            eyeEnemy.eyeEnemy.Width = W / 16; eyeEnemy.eyeEnemy.Height = H / 8;
            eyeEnemy.eyeEnemyLabal.Width = W / 22; eyeEnemy.eyeEnemyLabal.Height = H / 18;
            mashroomEnemy.mashroom.Width = W / 16;mashroomEnemy.mashroom.Height = H / 14;
            mashroomEnemy.mashroomLabal.Width = W / 22; mashroomEnemy.mashroomLabal.Height = H / 18;
            goblinEnemy.goblin.Width = W / 16; goblinEnemy.goblin.Height = H / 14;
            goblinEnemy.goblinLabal.Width = W / 22; goblinEnemy.goblinLabal.Height = H / 18;
            keyAndDoor.playerWin.Width = W / 5; keyAndDoor.playerWin.Height = H / 5;
            keyAndDoor.winMsg.FontSize = W / 30;
            keyAndDoor.door.Width = W / 10; keyAndDoor.door.Height = H / 4;
            keyAndDoor.key.Width = W / 16; keyAndDoor.key.Height = H / 14;

            if (keyAndDoor.doorRect.IntersectsWith(decor.blobkDownRect))
            {
                Canvas.SetLeft(keyAndDoor.door, Canvas.GetLeft(keyAndDoor.door) + W/5);
            }

            player.playerSpeed = H / 40;
            player.playerForceJump = H / 15;
        }

        private void WindowSizeChange(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetTop(player.player, 0);
            Canvas.SetTop(keyAndDoor.key, Canvas.GetTop(decor.ground3));
        }
    }
}



