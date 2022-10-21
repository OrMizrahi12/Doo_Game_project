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
        private PhysicsDecor decor = new PhysicsDecor();
        private Player player = new Player();
        private DispatcherTimer GameTimer = new DispatcherTimer();
        private GoblinEnemy goblinEnemy = new GoblinEnemy();
        private EyeEnemy eyeEnemy = new EyeEnemy();
        private GiftPlayer giftPlayer = new GiftPlayer();
        private MashroomEnemy mashroomEnemy = new MashroomEnemy();
        private PhaseToolsLevel phaseTools = new PhaseToolsLevel();
        private double H, W;

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
            H = this.ActualHeight; W = this.ActualWidth;
          
            ResponsiveManager();
            player.PlayerProgressLifeEvent(GameTimer);
            decor.SetGroundRects();
            player.SetPlayerRects();
            giftPlayer.SetGiftsRect();
            goblinEnemy.SetGoblinRect();
            eyeEnemy.SetEyeEnemyRect();
            mashroomEnemy.SetMashroomRect();
            phaseTools.SetPhaseToolsRect();
            decor.GroundPhysics(player, H);
            player.PlayerMoveController(H, W);
            decor.MoveScreen(player, goblinEnemy, eyeEnemy, giftPlayer, mashroomEnemy,phaseTools, H);
            decor.MakeBackGroundInfinity(H, W);
            decor.GroundLocator(player, W);
            decor.MoveDecor(player);
            goblinEnemy.GoblinController(player, decor, GameTimer);
            eyeEnemy.EyeEnemyController(player, decor, GameTimer);
            mashroomEnemy.MashroomController(player, decor, GameTimer); 
            giftPlayer.PlayerGiftTouchEvent(player, decor);
            phaseTools.PlayerTakeKey(player);
            phaseTools.PlayerOpenDoor(player,GameTimer, mainCanvas, W, H);
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
            decor.InitialDecor(mainCanvas,H, W, phaseTools);
            player.InitialPlayer(mainCanvas);
            goblinEnemy.InitialGoblin(mainCanvas,player);
            eyeEnemy.InitialEyeEnemy(mainCanvas , player);
            mashroomEnemy.InitialMashroom(mainCanvas, player);
            giftPlayer.InitialGifts(mainCanvas, decor);
            phaseTools.InitialPhaseTools(mainCanvas, decor);
            GameTimer.Start();
            phaseTools.playerWin.Visibility = Visibility.Hidden;
        }

        private void ResponsiveManager()
        {
            player.ResponsivePlayer(H, W);
            eyeEnemy.ResponsiveEyeEnemy(player, H, W);
            goblinEnemy.ResponsiveGoblin(decor,eyeEnemy,player,mashroomEnemy,H,W);
            mashroomEnemy.ResponsivMashroom(decor, eyeEnemy, goblinEnemy, player, W, H);
            decor.ResponsiveDecor(W,H);
            phaseTools.ResponsivePhaseTools(decor, H, W); 
        }

        private void WindowSizeChange(object sender, SizeChangedEventArgs e)
        {
            player.LocatePlayerAfterLoading();
            phaseTools.LocatePhaseToolsAfterLoading(decor);
        }
    }
}



