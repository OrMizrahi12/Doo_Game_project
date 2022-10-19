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
            decor.GroundPhysics(player, GameTimer,mainCanvas);
            player.PlayerMoveController(this.ActualHeight, this.ActualWidth);
            decor.MoveScreen(player, goblinEnemy, eyeEnemy, giftPlayer, mashroomEnemy, this.ActualHeight);
            decor.MakeBackGroundInfinity(this.ActualHeight, this.ActualWidth);
            decor.GroundLocator(player, this.ActualWidth);
            decor.MoveDecor(player, GameTimer, this.ActualHeight);
            goblinEnemy.GoblinController(player, decor, GameTimer);
            eyeEnemy.EyeEnemyController(player, decor, GameTimer);
            mashroomEnemy.MashroomController(player, decor, GameTimer); 
            giftPlayer.PlayerGiftTouchEvent(player, decor);
        
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
            else if (e.Key == Key.Enter)
            {
                ResetGame();

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
            decor.InitialDecor(mainCanvas, this.ActualHeight, this.ActualWidth);
            player.InitialPlayer(mainCanvas);
            goblinEnemy.InitialGoblin(mainCanvas);
            eyeEnemy.InitialEyeEnemy(mainCanvas);
            mashroomEnemy.InitialMashroom(mainCanvas);
            giftPlayer.InitialGifts(mainCanvas, decor);
            GameTimer.Start();
        }

        private void Responsive()
        {
            double H = this.ActualHeight;
            double W = this.ActualWidth;
            
            decor.background1.Height = H;
            decor.background2.Height = H;
            Canvas.SetTop(decor.ground1, H / 3); 
            Canvas.SetTop(decor.ground2, H / 2); 
            Canvas.SetTop(decor.ground3, H / 3.5);
            Canvas.SetTop(decor.flor1, H / 1.3);
            Canvas.SetTop(decor.flor2, H / 1.3);

            decor.ground1.Width = W / 8;
            decor.ground2.Width = W / 8;
            decor.ground3.Width = W / 8;
            decor.blockDown.Height = H / 2;
            decor.blockUp.Height = H /2;

            player.player.Width = W / 14;
            player.player.Height = H / 8;

            eyeEnemy.eyeEnemy.Width = W / 16;
            eyeEnemy.eyeEnemy.Height = H / 8;

            mashroomEnemy.mashroom.Width = W / 16;
            mashroomEnemy.mashroom.Height = H / 14;

            goblinEnemy.goblin.Width = W / 16;
            goblinEnemy.goblin.Height = H / 14;

        }
    }
}



