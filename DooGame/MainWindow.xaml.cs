using Akavache;
using DooGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private MenuInterface menuInterface = new MenuInterface();
        private double H, W;

        public MainWindow()
        {
            InitializeComponent();
            InitialGame();
            mainCanvas.Focus();
        }

        public void InitialGame()
        {
            ResetGame();
            mainCanvas.Focus();
            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
        }

        private void GameEngine(object sender, EventArgs e)
        {
            H = this.ActualHeight; W = this.ActualWidth;
            ResponsiveManager();
            player.PlayerProgressLifeEvent(GameTimer, menuInterface);
            decor.SetGroundRects();
            player.SetPlayerRects();
            giftPlayer.SetGiftsRect();
            goblinEnemy.SetGoblinRect();
            eyeEnemy.SetEyeEnemyRect();
            mashroomEnemy.SetMashroomRect();
            phaseTools.SetPhaseToolsRect();
            decor.GroundPhysics(player, H);
            player.PlayerMoveController(H, W);
            decor.MoveScreen(player, goblinEnemy, eyeEnemy, giftPlayer, mashroomEnemy, phaseTools, H);
            decor.MakeBackGroundInfinity(H, W);
            decor.GroundLocator(player, W);
            decor.MoveDecor(player);
            giftPlayer.PlayerGiftTouchEvent(player, decor);
            phaseTools.PlayerTakeKey(player);
            phaseTools.PlayerOpenDoor(player, GameTimer, mainCanvas, W, H, menuInterface);

            if (menuInterface.starterCanvas.Visibility == Visibility.Collapsed)
            {
                goblinEnemy.GoblinController(player, decor, GameTimer);
                eyeEnemy.EyeEnemyController(player, decor, GameTimer);
                mashroomEnemy.MashroomController(player, decor, GameTimer);
            }
               

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
            
            else if(e.Key == Key.S)
            {
                Settings.Default.SaveLevel = phaseTools.level;
                
            }
            else if(e.Key == Key.G)
            {
                phaseTools.level =  Settings.Default.SaveLevel;
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
            decor.InitialDecor(mainCanvas, H, W, phaseTools);
            player.InitialPlayer(mainCanvas);
            goblinEnemy.InitialGoblin(mainCanvas, player);
            eyeEnemy.InitialEyeEnemy(mainCanvas, player);
            mashroomEnemy.InitialMashroom(mainCanvas, player);
            giftPlayer.InitialGifts(mainCanvas, decor);
            phaseTools.InitialPhaseTools(mainCanvas, decor);
            menuInterface.InitialMenuInterface(GameTimer, mainCanvas, ResetGame);
            GameTimer.Start();
            phaseTools.playerWin.Visibility = Visibility.Hidden;
        }

        private void ResponsiveManager()
        {
            menuInterface.ResponsiveGui(H, W);
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

        private void save_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = File.Open(@"C:\Desktop\MainWindow.xaml", FileMode.Create);
            XamlWriter.Save(mainCanvas, fs);
            fs.Close();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = File.Open(@"C:\Desktop\MainWindow.xaml", FileMode.Open, FileAccess.Read);
            Canvas savedCanvas = XamlReader.Load(fs) as Canvas;
            fs.Close();

            mainCanvas.Children.Add(savedCanvas);

        }





    }



}



