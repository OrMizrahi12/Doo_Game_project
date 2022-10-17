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

        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
            ResetGame();
        }

        private void InitialGame()
        { 
            mainCanvas.Focus();
            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
        }

        private void GameEngine(object sender, EventArgs e)
        {
            decor.SetGroundRects();
            player.SetPlayerRects();
            giftPlayer.SetGiftsRect();
            goblinEnemy.SetGoblinRect();
            eyeEnemy.SetEyeEnemyRect();
            decor.GroundPhysics(player, GameTimer,mainCanvas);
            player.PlayerMoveController(mainCanvas, player);
            decor.MoveScreen(player, goblinEnemy, eyeEnemy, giftPlayer);
            decor.MakeBackGroundInfinity();
            decor.GroundLocator(player);
            decor.MoveDecor(player, GameTimer);
            goblinEnemy.GoblinController(player, decor, GameTimer);
            eyeEnemy.EyeEnemyController(player, decor, GameTimer);
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
            decor.InitialDecor(mainCanvas);
            player.InitialPlayer(mainCanvas);
            goblinEnemy.InitialGoblin(mainCanvas);
            eyeEnemy.InitialEyeEnemy(mainCanvas);
            giftPlayer.InitialGifts(mainCanvas, decor);
            GameTimer.Start();
        }
    }
}



